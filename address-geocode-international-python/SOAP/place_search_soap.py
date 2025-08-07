from suds.client import Client
from suds import WebFault
from suds.sudsobject import Object

class PlaceSearch:
    def __init__(self, license_key: str, is_live: bool, timeout_ms: int = 10000):
        """
        Initialize the PlaceSearch SOAP client.

        Parameters:
            license_key (str): Service Objects Address Geocode International license key.
            is_live (bool): whether to use live or trial endpoints.
            timeout_ms (int): SOAP call timeout in milliseconds.
        """
        self._timeout_s = timeout_ms / 1000.0
        self.license_key = license_key
        self._is_live = is_live

        # WSDL URLs
        self._primary_wsdl = (
            "https://sws.serviceobjects.com/AGI/soap.svc?wsdl"
            if is_live
            else "https://trial.serviceobjects.com/AGI/soap.svc?wsdl"
        )
        self._backup_wsdl = (
            "https://swsbackup.serviceobjects.com/AGI/soap.svc?wsdl"
            if is_live
            else "https://trialbackup.serviceobjects.com/AGI/soap.svc?wsdl"
        )

    def place_search(self,
                    single_line: str,
                    address1: str,
                    address2: str,
                    address3: str,
                    address4: str,
                    address5: str,
                    locality: str,
                    administrative_area: str,
                    postal_code: str,
                    country: str,
                    max_results: int,
                    search_type: str,
                    boundaries: str,
                    extras: str) -> Object:
        """
        Call PlaceSearch SOAP API using primary endpoint; on None response,
        WebFault, or Error.TypeCode == '3' falls back to the backup endpoint.
        returns: the suds response object
        raises RuntimeError: if both endpoints fail
        """

        # Common kwargs for both calls
        call_kwargs = dict(
            SingleLine=single_line,
            Address1=address1,
            Address2=address2,
            Address3=address3,
            Address4=address4,
            Address5=address5,
            Locality=locality,
            AdministrativeArea=administrative_area,
            PostalCode=postal_code,
            Country=country,
            MaxResults=max_results,
            SearchType=search_type,
            Boundaries=boundaries,
            Extras=extras,
            LicenseKey=self.license_key,
        )

        # Attempt primary
        try:
            client = Client(self._primary_wsdl)

            # Override endpoint URL if needed:
            response = client.service.PlaceSearch(**call_kwargs)

            # If response is None or fatal error code, trigger fallback
            if response is None or (hasattr(response, 'Error') and response.Error and response.Error.TypeCode == '3'):
                raise ValueError("Primary returned no result or fatal Error.TypeCode=3")

            return response

        except (WebFault, ValueError, Exception) as primary_ex:
            try:
                # Attempt backup
                client = Client(self._backup_wsdl, timeout=self._timeout_s)
                response = client.service.GetBestMatches(**call_kwargs)
                if response is None:
                    raise ValueError("Backup returned no result")
                return response

            except (WebFault, Exception) as backup_ex:
                msg = (
                    "Both primary and backup endpoints failed.\n"
                    f"Primary error: {primary_ex}\n"
                    f"Backup error: {backup_ex}"
                )
                raise RuntimeError(msg)
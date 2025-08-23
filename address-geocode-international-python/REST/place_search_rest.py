'''
Service Objects - AGI PlaceSearch Client

This module provides the place_search function to search and geocode international places
using Service Objects' AGI PlaceSearch REST API. It supports trial/live environments,
fallback handling, and extended parameter support.

Functions:
    place_search(single_line: str,
                address1: str,
                address2: str,
                address3: str,
                address4: str,
                address5: str,
                locality: str,
                administrative_area: str,
                postal_code: str,
                country: str,
                boundaries: str,
                max_results: int,
                search_type: str,
                extras: str,
                license_key: str,
                is_live: bool) -> dict:
'''

import requests

# Endpoint URLs for AGI PlaceSearch
PRIMARY_URL = "https://sws.serviceobjects.com/AGI/api.svc/json/PlaceSearch"
BACKUP_URL = "https://swsbackup.serviceobjects.com/AGI/api.svc/PlaceSearch"
TRIAL_URL = "https://trial.serviceobjects.com/AGI/api.svc/json/PlaceSearch"

def place_search(single_line: str,
                address1: str,
                address2: str,
                address3: str,
                address4: str,
                address5: str,
                locality: str,
                administrative_area: str,
                postal_code: str,
                country: str,
                boundaries: str,
                max_results: int,
                search_type: str,
                extras: str,
                license_key: str,
                is_live: bool) -> dict:
    """
    Calls the AGI PlaceSearch API and returns the parsed response.

    Parameters:
        single_line (str): Full address or place name (takes priority).
        address1–5 (str): Optional multiline address input fields.
        locality (str): City or locality.
        administrative_area (str): State, province, or region.
        postal_code (str): ZIP or postal code.
        country (str): ISO-2 country code.
        boundaries (str): Optional boundary filter.
        max_results (int): Number of results to return.
        search_type (str): Type of search: Address, Locality, PostalCode, etc.
        extras (str): Optional additional flags.
        license_key (str): AGI API key.
        is_live (bool): True for live endpoint; False for trial.

    Returns:
        dict: Parsed JSON response from the API.

    Raises:
        RuntimeError: If the API returns an error payload.
        requests.RequestException: On network/HTTP failures (trial mode).
    """

    # Prepare query parameters for AGI API
    params = {
        "SingleLine": single_line,
        "Address1": address1,
        "Address2": address2,
        "Address3": address3,
        "Address4": address4,
        "Address5": address5,
        "Locality": locality,
        "AdministrativeArea": administrative_area,
        "PostalCode": postal_code,
        "Country": country,
        "Boundaries": boundaries,
        "MaxResults": max_results,
        "SearchType": search_type,
        "Extras": extras,
        "LicenseKey": license_key
    }

    # Select the base URL: production vs trial
    url = PRIMARY_URL if is_live else TRIAL_URL

    # Attempt primary (or trial) endpoint first
    try:
        response = requests.get(url, params=params, timeout=10)
        response.raise_for_status()
        data = response.json()

        # If API returned an error in JSON payload, trigger fallback
        error = getattr(response, 'Error', None)
        if not (error is None or getattr(error, 'TypeCode', None) != "3"):
            if is_live:
                # Try backup URL when live
                response = requests.get(BACKUP_URL, params=params, timeout=10)
                data = response.json()

                # If still error, propagate exception
                if "Error" in data:
                    raise RuntimeError(f"AGI PlaceSearch backup error: {data['Error']}")
            else:
                # Trial mode should not fallback; error is terminal
                return data

        # Success: return parsed JSON data
        return data

    except requests.RequestException as req_exc:
        # Network or HTTP-level error occurred
        if is_live:
            try:
                # Fallback to backup URL on network failure
                response = requests.get(BACKUP_URL, params=params, timeout=10)
                response.raise_for_status()
                data = response.json()
                return data
            except Exception as fallback_exc:
                raise RuntimeError("AGI PlaceSearch unreachable on both endpoints") from fallback_exc

        else:
            # In trial mode, propagate the network exception
            raise RuntimeError(f"AGI trial error: {str(req_exc)}") from req_exc

using address_geocode_international_dot_net.REST;
using AGIService;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace address_geocode_international_dot_net.SOAP
{
    /// <summary>
    /// SOAP client wrapper for the ServiceObjects AGI PlaceSearch operation.
    /// Handles SOAP requests to primary and backup endpoints, supporting both trial and live modes.
    /// Provides async invocation with resiliency via failover mechanism.
    /// </summary>
    public class PlaceSearchValidation
    {
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AGI/soap.svc/SOAP";
        private const string BackupBaseUrl = "https://wsbackup.serviceobjects.com/AGI/soap.svc/SOAP";
        private const string TrailBaseUrl = "https://trial.serviceobjects.com/AGI/soap.svc/SOAP";

        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Initializes the PlaceSearch SOAP client with endpoint configuration based on mode (live or trial).
        /// </summary>
        /// <param name="isLive">True for live (production) endpoint; false for trial usage.</param>
        public PlaceSearchValidation(bool isLive)
        {
            // Read timeout (milliseconds) and isLive flag
            _timeoutMs = 10000;
            _isLive = isLive;

            // Depending on isLive, pick the correct SOAP endpoint URLs
            if (_isLive)
            {
                _primaryUrl = LiveBaseUrl;
                _backupUrl = BackupBaseUrl;
            }
            else
            {
                _primaryUrl = TrailBaseUrl;
                _backupUrl = TrailBaseUrl;
            }

            if (string.IsNullOrWhiteSpace(_primaryUrl))
                throw new InvalidOperationException("Primary URL not set. Check endpoint configuration.");

            if (string.IsNullOrWhiteSpace(_backupUrl))
                throw new InvalidOperationException("Backup URL not set. Check endpoint configuration.");
        }


        /// <summary>
        /// Executes the PlaceSearch SOAP request asynchronously.
        /// Attempts call to primary endpoint first; upon failure, retries with backup URL.
        /// </summary>
        /// <param name="SingleLine">Single-line address input. - Optional</param>
        /// <param name="Address1">Address line 1. - Optional</param>
        /// <param name="Address2">Address line 2. - Optional</param>
        /// <param name="Address3">Address line 3. - Optional</param>
        /// <param name="Address4">Address line 4. - Optional</param>
        /// <param name="Address5">Address line 5. - Optional</param>
        /// <param name="Locality">City or locality. - Optional</param>
        /// <param name="AdministrativeArea">State or province. - Optional</param>
        /// <param name="PostalCode">Zip or postal code. - Optional</param>
        /// <param name="Country">Country ISO code (e.g., "US", "CA"). - Optional</param>
        /// <param name="Boundaries">Geolocation search boundaries. - Optional</param>
        /// <param name="MaxResults">Maximum number of results to return. - Optional</param>
        /// <param name="SearchType">Specifies place type to search for. - Optional</param>
        /// <param name="Extras">Additional search attributes. - Optional</param>
        /// <param name="LicenseKey">Service Objects license key (live or trial). - Required</param>
        /// <returns><see cref="ResponseObject"/> containing matched place search results.</returns>
        /// <exception cref="Exception">Throws when both primary and backup endpoints fail.</exception>
        public async Task<ResponseObject> PlaceSearch(
            string SingleLine,
            string Address1,
            string Address2,
            string Address3,
            string Address4,
            string Address5,
            string Locality,
            string AdministrativeArea,
            string PostalCode,
            string Country,
            string Boundaries,
            string MaxResults,
            string SearchType,
            string Extras,
            string LicenseKey)
        {
            AGISoapServiceClient clientPrimary = null;
            AGISoapServiceClient clientBackup = null;

            try
            {
                // 1) Attempt Primary
                clientPrimary = new AGISoapServiceClient();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);
                
                
                ResponseObject response = await clientPrimary.PlaceSearchAsync(
                    SingleLine,
                    Address1,
                    Address2,
                    Address3,
                    Address4,
                    Address5,
                    Locality,
                    AdministrativeArea,
                    PostalCode,
                    Country,
                    Boundaries,
                    MaxResults,
                    SearchType,
                    Extras,
                    LicenseKey
                ).ConfigureAwait(false);

                // Client requirement: failover only if response is null or error TypeCode == "3"
                if (response == null)
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal TypeCode=3 error for PlaceSearch");
                }

                return response;
            }
            catch (Exception primaryEx)
            {
                try
                {
                    // 2) Fallback: Attempt Backup
                    clientBackup = new AGISoapServiceClient();
                    clientBackup.Endpoint.Address = new System.ServiceModel.EndpointAddress(_backupUrl);
                    clientBackup.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                    return await clientBackup.PlaceSearchAsync(
                        SingleLine,
                        Address1,
                        Address2,
                        Address3,
                        Address4,
                        Address5,
                        Locality,
                        AdministrativeArea,
                        PostalCode,
                        Country,
                        Boundaries,
                        MaxResults,
                        SearchType,
                        Extras,
                        LicenseKey
                    ).ConfigureAwait(false);
                }
                catch (Exception backupEx)
                {
                    throw new Exception(
                        "Both primary and backup PlaceSearch endpoints failed.\n" +
                        "Primary error: " + primaryEx.Message + "\n" +
                        "Backup error: " + backupEx.Message);
                }
                finally
                {
                    clientBackup?.Close();
                }
            }
            finally
            {
                clientPrimary?.Close();
            }
        }


    }
}

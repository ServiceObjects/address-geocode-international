using AGIService;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace address_geocode_international_dot_net.SOAP
{
    /// <summary>
    /// SOAP client wrapper for the ServiceObjects AGI ReverseSearch operation.
    /// Handles requests to both primary and backup endpoints, supporting both live and trial modes.
    /// Includes automatic failover logic, secure SOAP binding, and API timeout settings.
    /// </summary>
    public class ReverseSearchValidation
    {
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AGI/soap.svc/SOAP";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AGI/soap.svc/SOAP";
        private const string TrailBaseUrl = "https://trial.serviceobjects.com/AGI/soap.svc/SOAP";

        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Initializes endpoint URLs and sets request timeout.
        /// </summary>
        /// <param name="isLive">
        /// Indicates if live service endpoints should be used (vs. trial endpoints).
        /// True for live, false for trial.
        /// </param>
        public ReverseSearchValidation(bool isLive)
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
        /// Asynchronously calls the AGI ReverseSearch SOAP method.
        /// Automatically attempts the backup endpoint if the primary call fails or yields no results.
        /// </summary>
        /// <param name="Latitude">Latitude for geographic search (in decimal degrees).</param>
        /// <param name="Longitude">Longitude for geographic search (in decimal degrees).</param>
        /// <param name="LicenseKey">Required license key for Service Objects API.</param>
        /// <param name="SearchRadius">Optional. Search radius in kilometers (default: "10").</param>
        /// <param name="Country">Optional. Country ISO code (default: "US").</param>
        /// <param name="MaxResults">Optional. Maximum number of results to return (default: "10").</param>
        /// <param name="SearchType">Optional. Filter results by type (default: "All").</param>
        /// <returns>
        /// A <see cref="ResponseObject"/> representing results from the ReverseSearch service.
        /// Throws an exception if both endpoints fail or return no results.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if neither the primary nor backup endpoint succeeds or returns results.
        /// Contains error details from both attempts.
        /// </exception>
        public async Task<ResponseObject> ReverseSearch(
            string Latitude,
            string Longitude,
            string LicenseKey,
            string SearchRadius,
            string Country,
            string MaxResults,
            string SearchType)
        {
            AGISoapServiceClient clientPrimary = null;
            AGISoapServiceClient clientBackup = null;

            try
            {
                // 1) Attempt primary endpoint
                clientPrimary = new AGISoapServiceClient();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                ResponseObject response = await clientPrimary.ReverseSearchAsync(
                    Latitude,
                    Longitude,
                    SearchRadius,
                    Country,
                    MaxResults,
                    SearchType,
                    LicenseKey
                ).ConfigureAwait(false);

                // Client requirement: failover only if response is null or error TypeCode == "3"
                if (response == null)
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal TypeCode=3 error for ReverseSearch");
                }

                return response;
            }
            catch (Exception primaryEx)
            {
                try
                {
                    // 2) Attempt backup endpoint
                    clientBackup = new AGISoapServiceClient();
                    clientBackup.Endpoint.Address = new System.ServiceModel.EndpointAddress(_backupUrl);
                    clientBackup.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                    return await clientBackup.ReverseSearchAsync(
                        Latitude,
                        Longitude,
                        SearchRadius,
                        Country,
                        MaxResults,
                        SearchType,
                        LicenseKey
                    ).ConfigureAwait(false);
                }
                catch (Exception backupEx)
                {
                    throw new Exception(
                        "Both primary and backup ReverseSearch SOAP endpoints failed.\n" +
                        "Primary error: " + primaryEx.Message + "\n" +
                        "Backup error:  " + backupEx.Message, backupEx);
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

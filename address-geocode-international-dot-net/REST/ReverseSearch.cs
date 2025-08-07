using System;
using System.Threading.Tasks;

namespace address_geocode_international_dot_net.REST
{
    /// <summary>
    /// Client for AGI ReverseSearch REST operation, supporting sync and async calls.
    /// Handles primary, backup, and trial endpoints, fallback logic, URL encoding, and JSON deserialization.
    /// </summary>
    public static class ReverseSearchClient
    {
        // Base URL constants for production, backup, and trial API endpoints
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AGI/api.svc/json/ReverseSearch";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AGI/api.svc/json/ReverseSearch";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/AGI/api.svc/json/ReverseSearch";

        /// <summary>
        /// Synchronously invoke the AGI ReverseSearch API.
        /// Performs a live or trial request; if a live request fails, retries with the backup endpoint.
        /// </summary>
        /// <param name="input">Input parameters (coordinates, search type, license key, isLive).</param>
        /// <returns>Deserialized <see cref="AGIReverseSearchResponse"/>.</returns>
        public static AGIReverseSearchResponse Invoke(ReverseSearchInput input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            AGIReverseSearchResponse response = Helper.HttpGet<AGIReverseSearchResponse>(url, input.TimeoutSeconds);

            // If using live endpoint and initial response is invalid, try backup endpoint
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                AGIReverseSearchResponse fallbackResponse = Helper.HttpGet<AGIReverseSearchResponse>(fallbackUrl, input.TimeoutSeconds);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously invoke the AGI ReverseSearch API.
        /// Performs a live or trial request; if a live request fails, retries with the backup endpoint.
        /// </summary>
        /// <param name="input">Input parameters (coordinates, search type, license key, isLive).</param>
        /// <returns>Deserialized <see cref="AGIReverseSearchResponse"/>.</returns>
        public static async Task<AGIReverseSearchResponse> InvokeAsync(ReverseSearchInput input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            AGIReverseSearchResponse response = await Helper.HttpGetAsync<AGIReverseSearchResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // Fallback to backup endpoint if needed (live requests only)
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                AGIReverseSearchResponse fallbackResponse = await Helper.HttpGetAsync<AGIReverseSearchResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return IsValid(fallbackResponse) ? fallbackResponse : response;
            }

            return response;
        }

        /// <summary>
        /// Determines response validity (non-null, no error payload).
        /// </summary>
        /// <param name="response">API response to check.</param>
        /// <returns>True if response is valid; otherwise, false.</returns>
        private static bool IsValid(AGIReverseSearchResponse response) =>
           response?.Error == null || response.Error.TypeCode != "3";


        /// <summary>
        /// Constructs the request URL with all query string parameters, ensuring values are URL-encoded.
        /// </summary>
        /// <param name="input">Request parameters (latitude, longitude, etc.).</param>
        /// <param name="baseUrl">API base endpoint URL.</param>
        /// <returns>Full query URL for the API call.</returns>
        private static string BuildUrl(ReverseSearchInput input, string baseUrl)
        {
            return baseUrl + "?" +
                   $"Latitude={Helper.UrlEncode(input.Latitude)}" +
                   $"&Longitude={Helper.UrlEncode(input.Longitude)}" +
                   $"&SearchRadius={Helper.UrlEncode(input.SearchRadius)}" +
                   $"&Country={Helper.UrlEncode(input.Country)}" +
                   $"&MaxResults={Helper.UrlEncode(input.MaxResults)}" +
                   $"&SearchType={Helper.UrlEncode(input.SearchType)}" +
                   $"&LicenseKey={Helper.UrlEncode(input.LicenseKey)}";
        }

        /// <summary>
        /// Input parameters for a ReverseSearch API call.
        /// </summary>
        /// <param name="Latitude">Latitude value for reverse search query. - Required</param>
        /// <param name="Longitude">Longitude value for reverse search query. - Required</param>
        /// <param name="SearchRadius">The maximum search radius distance set in kilometers. Defaults to 1. Maximum value of 50. - Optional</param>
        /// <param name="Country">Country of the location. - Optional</param>
        /// <param name="MaxResults">Sets the maximum number of results the operation is allowed to return. Acceptable values range from 1 to 10. - Optional</param>
        /// <param name="SearchType">Type of search to perform ("All" by default). - Optional</param>
        /// <param name="LicenseKey">Service Objects API license key. - Required</param>
        /// <param name="IsLive">Flag for live (primary/backup endpoints) vs. trial. - Required</param>
        /// <param name="TimeoutSeconds">Timeout for the API request (default: 15 seconds). - Optional</param>
        public record ReverseSearchInput(
            string Latitude,
            string Longitude,
            string SearchRadius,
            string Country = "",
            string MaxResults = "",
            string SearchType = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}

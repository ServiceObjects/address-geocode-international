using System.Threading.Tasks;

namespace address_geocode_international_dot_net.REST
{
    /// <summary>
    /// Client for PlaceSearch operation via ServiceObjects AGI REST API.
    /// Supports fallback to backup URL if live service fails.
    /// </summary>
    public static class PlaceSearchClient
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/AGI/api.svc/json/PlaceSearch";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/AGI/api.svc/json/PlaceSearch";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/AGI/api.svc/json/PlaceSearch";

        /// <summary>
        /// Synchronously call the PlaceSearchJson endpoint.
        /// </summary>
        /// <param name="input">Request parameters (address, license key, isLive).</param>
        /// <returns>Deserialized <see cref="AGIPlaceSearchResponse"/>.</returns>
        public static AGIPlaceSearchResponse Invoke(PlaceSearchInput input)
        {
            // Use appropriate base URL depending on live/trial flag
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);

            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            AGIPlaceSearchResponse response = Helper.HttpGet<AGIPlaceSearchResponse>(url, input.TimeoutSeconds);

            // Fallback on error payload in live mode
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                AGIPlaceSearchResponse fallbackResponse = Helper.HttpGet<AGIPlaceSearchResponse>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously call the PlaceSearchJson endpoint.
        /// </summary>
        /// <param name="input">Request parameters (address, license key, isLive).</param>
        /// <returns>Deserialized <see cref="AGIPlaceSearchResponse"/>.</returns>
        public static async Task<AGIPlaceSearchResponse> InvokeAsync(PlaceSearchInput input)
        {
            // Use appropriate base URL depending on live/trial flag
            var url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);

            // Perform HTTP GET request asynchronously and deserialize response
            AGIPlaceSearchResponse response = await Helper.HttpGetAsync<AGIPlaceSearchResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // In live mode, attempt backup URL if primary response is invalid
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                AGIPlaceSearchResponse fallbackResponse = await Helper.HttpGetAsync<AGIPlaceSearchResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Build the full request URL with encoded query string.
        /// </summary>
        /// <param name="input">Input data for PlaceSearch.</param>
        /// <param name="baseUrl">Base endpoint URL.</param>
        /// <returns>Complete request URL.</returns>
        private static string BuildUrl(PlaceSearchInput input, string baseUrl)
        {
            return baseUrl + "?" +
                   $"SingleLine={Helper.UrlEncode(input.SingleLine)}" +
                   $"Address1={Helper.UrlEncode(input.Address1)}" +
                   $"Address2={Helper.UrlEncode(input.Address2)}" +
                   $"Address3={Helper.UrlEncode(input.Address3)}" +
                   $"Address4={Helper.UrlEncode(input.Address4)}" +
                   $"Address5={Helper.UrlEncode(input.Address5)}" +
                   $"Locality={Helper.UrlEncode(input.Locality)}" +
                   $"AdministrativeArea={Helper.UrlEncode(input.AdministrativeArea)}" +
                   $"PostalCode={Helper.UrlEncode(input.PostalCode)}" +
                   $"&Country={Helper.UrlEncode(input.Country)}" +
                   $"&Boundaries={Helper.UrlEncode(input.Boundaries)}" +
                   $"&MaxResults={Helper.UrlEncode(input.MaxResults)}" +
                   $"&SearchType={Helper.UrlEncode(input.SearchType)}" +
                   $"&Extras={Helper.UrlEncode(input.Extras)}" +
                   $"&LicenseKey={Helper.UrlEncode(input.LicenseKey)}";
        }

        /// <summary>
        /// Checks if the response is valid (non-null and no error).
        /// </summary>
        /// <param name="response">Response to validate.</param>
        /// <returns>True if valid; otherwise false.</returns>
        private static bool IsValid(AGIPlaceSearchResponse response) => response?.Error == null || response.Error.TypeCode != "3";


        /// <summary>
        /// Input parameters for the PlaceSearch operation.
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
        /// <param name="LicenseKey">Service Objects license key. - Required</param>
        /// <param name="IsLive">True to use production+backup; false for trial only. - Required</param>
        /// <param name="TimeoutSeconds">Request timeout in seconds (default: 15).</param>
        public record PlaceSearchInput(
            string SingleLine = "",
            string Address1 = "",
            string Address2 = "",
            string Address3 = "",
            string Address4 = "",
            string Address5 = "",
            string Locality = "",
            string AdministrativeArea = "",
            string PostalCode = "",
            string Country = "",
            string Boundaries = "",
            string MaxResults = "",
            string SearchType = "",
            string Extras = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}
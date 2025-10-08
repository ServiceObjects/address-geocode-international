import axios from 'axios';
import querystring from 'querystring';
import { PSResponse } from './agi_response.js';

/**
 * @constant
 * @type {string}
 * @description The base URL for the live ServiceObjects Address Geocode International (AGI) API service.
 */
const LiveBaseUrl = 'https://sws.serviceobjects.com/agi/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the backup ServiceObjects Address Geocode International (AGI) API service.
 */
const BackupBaseUrl = 'https://swsbackup.serviceobjects.com/agi/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the trial ServiceObjects Address Geocode International (AGI) API service.
 */
const TrialBaseUrl = 'https://trial.serviceobjects.com/agi/api.svc/json/';

/**
 * <summary>
 * Checks if a response from the API is valid by verifying that it either has no Error object
 * or the Error.TypeCode is not equal to '3'.
 * </summary>
 * <param name="response" type="Object">The API response object to validate.</param>
 * <returns type="boolean">True if the response is valid, false otherwise.</returns>
 */
const isValid = (response) => !response?.Error || response.Error.TypeCode !== '3';

/**
 * <summary>
 * Constructs a full URL for the PlaceSearch API endpoint by combining the base URL
 * with query parameters derived from the input parameters.
 * </summary>
 * <param name="params" type="Object">An object containing all the input parameters.</param>
 * <param name="baseUrl" type="string">The base URL for the API service (live, backup, or trial).</param>
 * <returns type="string">The constructed URL with query parameters.</returns>
 */
const buildUrl = (params, baseUrl) =>
    `${baseUrl}PlaceSearch?${querystring.stringify(params)}`;

/**
 * <summary>
 * Performs an HTTP GET request to the specified URL with a given timeout.
 * </summary>
 * <param name="url" type="string">The URL to send the GET request to.</param>
 * <param name="timeoutSeconds" type="number">The timeout duration in seconds for the request.</param>
 * <returns type="Promise<PSResponse>">A promise that resolves to a PSResponse object containing the API response data.</returns>
 * <exception cref="Error">Thrown if the HTTP request fails, with a message detailing the error.</exception>
 */
const httpGet = async (url, timeoutSeconds) => {
    try {
        const response = await axios.get(url, { timeout: timeoutSeconds * 1000 });
        return new PSResponse(response.data);
    } catch (error) {
        throw new Error(`HTTP request failed: ${error.message}`);
    }
};

/**
 * <summary>
 * Provides functionality to call the ServiceObjects Address Geocode International (AGI) API's PlaceSearch endpoint,
 * retrieving geocoded location information for a given address or place with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
const PlaceSearchClient = {
    /**
     * <summary>
     * Asynchronously invokes the PlaceSearch API endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.TypeCode == '3') in live mode.
     * </summary>
     * @param {string} SingleLine - The full address on one line. Optional; for best results, use parsed inputs.
     * @param {string} Address1 - Address Line 1 of the international address. Optional.
     * @param {string} Address2 - Address Line 2 of the international address. Optional.
     * @param {string} Address3 - Address Line 3 of the international address. Optional.
     * @param {string} Address4 - Address Line 4 of the international address. Optional.
     * @param {string} Address5 - Address Line 5 of the international address. Optional.
     * @param {string} Locality - The name of the locality (e.g., city, town). Optional.
     * @param {string} AdministrativeArea - The administrative area (e.g., state, province). Optional.
     * @param {string} PostalCode - The postal code. Optional.
     * @param {string} Country - The country name or ISO 3166-1 Alpha-2/Alpha-3 code. Required.
     * @param {string} Boundaries - A comma-delimited list of coordinates for search boundaries. Optional; not currently used.
     * @param {string} MaxResults - Maximum number of results (1-10). Defaults to '10'.
     * @param {string} SearchType - Type of search (e.g., 'BestMatch', 'All'). Defaults to 'BestMatch'.
     * @param {string} Extras - Comma-delimited list of extra features. Optional.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @returns {Promise<PSResponse>} - A promise that resolves to a PSResponse object.
     */
    async invokeAsync(SingleLine, Address1, Address2, Address3, Address4, Address5, Locality, AdministrativeArea, PostalCode, Country, Boundaries, MaxResults = '10', SearchType = 'BestMatch', Extras, LicenseKey, isLive = true, timeoutSeconds = 15) {
        const params = {
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
        };

        // Remove null/undefined params to avoid empty query params
        //Object.keys(params).forEach(key => params[key] == null && delete params[key]);

        const url = buildUrl(params, isLive ? LiveBaseUrl : TrialBaseUrl);
        let response = await httpGet(url, timeoutSeconds);

        if (isLive && !isValid(response)) {
            const fallbackUrl = buildUrl(params, BackupBaseUrl);
            const fallbackResponse = await httpGet(fallbackUrl, timeoutSeconds);
            return fallbackResponse;
        }
        return response;
    },

    /**
     * <summary>
     * Synchronously invokes the PlaceSearch API endpoint by wrapping the async call
     * and awaiting its result immediately.
     * </summary>
     * @param {string} SingleLine - The full address on one line. Optional; for best results, use parsed inputs.
     * @param {string} Address1 - Address Line 1 of the international address. Optional.
     * @param {string} Address2 - Address Line 2 of the international address. Optional.
     * @param {string} Address3 - Address Line 3 of the international address. Optional.
     * @param {string} Address4 - Address Line 4 of the international address. Optional.
     * @param {string} Address5 - Address Line 5 of the international address. Optional.
     * @param {string} Locality - The name of the locality (e.g., city, town). Optional.
     * @param {string} AdministrativeArea - The administrative area (e.g., state, province). Optional.
     * @param {string} PostalCode - The postal code. Optional.
     * @param {string} Country - The country name or ISO 3166-1 Alpha-2/Alpha-3 code. Required.
     * @param {string} Boundaries - A comma-delimited list of coordinates for search boundaries. Optional; not currently used.
     * @param {string} MaxResults - Maximum number of results (1-10). Defaults to '10'.
     * @param {string} SearchType - Type of search (e.g., 'BestMatch', 'All'). Defaults to 'BestMatch'.
     * @param {string} Extras - Comma-delimited list of extra features. Optional.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @returns {PSResponse} - A PSResponse object with geocoded location details or an error.
     */
    invoke(SingleLine, Address1, Address2, Address3, Address4, Address5, Locality, AdministrativeArea, PostalCode, Country, Boundaries, MaxResults = '10', SearchType = 'BestMatch', Extras, LicenseKey, isLive = true, timeoutSeconds = 15) {
        return (async () => await this.invokeAsync(
            SingleLine, Address1, Address2, Address3, Address4, Address5, Locality, AdministrativeArea, PostalCode, Country, Boundaries, MaxResults, SearchType, Extras, LicenseKey, isLive, timeoutSeconds
        ))();
    }
};

export { PlaceSearchClient, PSResponse };
import axios from 'axios';
import querystring from 'querystring';
import { RSResponse } from './agi_response.js';

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
 * Constructs a full URL for the ReverseSearch API endpoint by combining the base URL
 * with query parameters derived from the input parameters.
 * </summary>
 * <param name="params" type="Object">An object containing all the input parameters.</param>
 * <param name="baseUrl" type="string">The base URL for the API service (live, backup, or trial).</param>
 * <returns type="string">The constructed URL with query parameters.</returns>
 */
const buildUrl = (params, baseUrl) =>
    `${baseUrl}ReverseSearch?${querystring.stringify(params)}`;

/**
 * <summary>
 * Performs an HTTP GET request to the specified URL with a given timeout.
 * </summary>
 * <param name="url" type="string">The URL to send the GET request to.</param>
 * <param name="timeoutSeconds" type="number">The timeout duration in seconds for the request.</param>
 * <returns type="Promise<RSResponse>">A promise that resolves to an RSResponse object containing the API response data.</returns>
 * <exception cref="Error">Thrown if the HTTP request fails, with a message detailing the error.</exception>
 */
const httpGet = async (url, timeoutSeconds) => {
    try {
        const response = await axios.get(url, { timeout: timeoutSeconds * 1000 });
        return new RSResponse(response.data);
    } catch (error) {
        throw new Error(`HTTP request failed: ${error.message}`);
    }
};

/**
 * <summary>
 * Provides functionality to call the ServiceObjects Address Geocode International (AGI) API's ReverseSearch endpoint,
 * retrieving address or place information for given coordinates with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
const ReverseSearchClient = {
    /**
     * <summary>
     * Asynchronously invokes the ReverseSearch API endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.TypeCode == '3') in live mode.
     * </summary>
     * @param {string} Latitude - The latitude of the location (decimal, up to 7 decimal places). Required.
     * @param {string} Longitude - The longitude of the location (decimal, up to 7 decimal places). Required.
     * @param {string} SearchRadius - The maximum search radius in kilometers (1-50). Defaults to '1'.
     * @param {string} Country - The country name or ISO 3166-1 Alpha-2/Alpha-3 code. Optional.
     * @param {string} MaxResults - Maximum number of results (1-10). Defaults to '10'.
     * @param {string} SearchType - Type of search (e.g., 'BestMatch', 'All'). Required.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @returns {Promise<RSResponse>} - A promise that resolves to an RSResponse object.
     */
    async invokeAsync(Latitude, Longitude, SearchRadius = '1', Country, MaxResults = '10', SearchType, LicenseKey, isLive = true, timeoutSeconds = 15) {
        const params = {
            Latitude,
            Longitude,
            SearchRadius,
            Country,
            MaxResults,
            SearchType,
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
     * Synchronously invokes the ReverseSearch API endpoint by wrapping the async call
     * and awaiting its result immediately.
     * </summary>
     * @param {string} Latitude - The latitude of the location (decimal, up to 7 decimal places). Required.
     * @param {string} Longitude - The longitude of the location (decimal, up to 7 decimal places). Required.
     * @param {string} SearchRadius - The maximum search radius in kilometers (1-50). Defaults to '1'.
     * @param {string} Country - The country name or ISO 3166-1 Alpha-2/Alpha-3 code. Optional.
     * @param {string} MaxResults - Maximum number of results (1-10). Defaults to '10'.
     * @param {string} SearchType - Type of search (e.g., 'BestMatch', 'All'). Required.
     * @param {string} LicenseKey - Your license key to use the service. Required.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers. Defaults to true.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service. Defaults to 15.
     * @returns {RSResponse} - An RSResponse object with address or place details or an error.
     */
    invoke(Latitude, Longitude, SearchRadius = '1', Country, MaxResults = '10', SearchType, LicenseKey, isLive = true, timeoutSeconds = 15) {
        return (async () => await this.invokeAsync(
            Latitude, Longitude, SearchRadius, Country, MaxResults, SearchType, LicenseKey, isLive, timeoutSeconds
        ))();
    }
};

export { ReverseSearchClient, RSResponse };
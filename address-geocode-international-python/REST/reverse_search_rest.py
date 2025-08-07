'''
Service Objects, Inc.

This module provides the reverse_search function to perform reverse geocoding
using Service Objects' AGI ReverseSearch API. It handles live/trial endpoints,
fallback logic, and JSON parsing.

Functions:
    reverse_search(latitude: float,
                longitude: float,
                search_radius: int,
                country: str,
                max_results: int,
                search_type: str,
                license_key: str,
                is_live: bool) -> dict
'''

import requests  # HTTP client for RESTful API calls

# Endpoint URLs for AV3 service
PRIMARY_URL = "https://sws.serviceobjects.com/AGI/api.svc/json/ReverseSearch"
BACKUP_URL = "https://swsbackup.serviceobjects.com/AGI/api.svc/ReverseSearch"
TRIAL_URL = "https://trial.serviceobjects.com/AGI/api.svc/json/ReverseSearch"

def reverse_search(latitude: float,
                longitude: float,
                search_radius: int,
                country: str,
                max_results: int,
                search_type: str,
                license_key: str,
                is_live: bool) -> dict:
    """
    Call AGI ReverseSearch API and return location results.

    Parameters:
        latitude (float): Latitude coordinate.
        longitude (float): Longitude coordinate.
        search_radius (int): The maximum search radius distance set in kilometers. Defaults to 1. Maximum value of 50.
        country (str): The preferred name of the country or the ISO 3166-1 Alpha-2 Country Code or the Alpha-3 Country Code.
        max_results (int): Maximum number of returned locations.
        search_type (str): The name of the type of search you want to perform for the given address or place.
        license_key (str): Service Objects license key.
        is_live (bool): True for production, False for trial endpoint.

    Returns:
        dict: Parsed JSON response with location data or error info.

    Raises:
        RuntimeError: If the API returns an error payload.
        requests.RequestException: On network/HTTP failures (trial mode).
    """

    # Prepare query parameters for AGI API
    params = {
        "Latitude": latitude,
        "Longitude": longitude,
        "SearchRadius": search_radius,
        "Country": country,
        "MaxResults": max_results,
        "SearchType": search_type,
        "LicenseKey": license_key
    }

    # Select the base URL: production vs trial
    url = PRIMARY_URL if is_live else TRIAL_URL

    try:
        # Attempt primary (or trial) endpoint first
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
                    raise RuntimeError(f"AGI ReverseSearch error: {data['Error']}")
            else:
                # Trial mode should not fallback; error is terminal
                raise RuntimeError(f"AGI trial error: {data['Error']}")
        
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
                if "Error" in data:
                    # Both primary and backup failed; escalate
                    raise RuntimeError(f"AGI ReverseSearch backup error: {data['Error']}")
                return data
            except Exception as backup_exc:
                raise RuntimeError("AGI ReverseSearch service unreachable on both endpoints") from backup_exc

        else:
            # In trial mode, propagate the network exception
            raise RuntimeError(f"AGI trial error: {str(req_exc)}") from req_exc

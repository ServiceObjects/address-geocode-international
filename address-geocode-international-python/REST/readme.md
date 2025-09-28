![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGI - Address Geocode International

DOTS Address Geocode International ("AGI") is a high-performance SDK for consuming Service Objects' international geocoding API. AGI enables you to find coordinates and information for global addresses, places, and points of interest, as well as perform reverse geocoding to retrieve address/location details from latitude and longitude.

AGI is designed for easy integration into applications, supporting both PlaceSearch and ReverseSearch operations with robust models, async/sync usage, and example console applications.

## [Service Objects Website](https://serviceobjects.com)

# AGI - PlaceSearch

Find coordinates and information for a place, address, or POI (Point of Interest) anywhere in the world.

### [PlaceSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-placesearch/)

## Library Usage

```
# 1. Build the input.
#
# Fields:
#     single_line
#     address1 
#     address2
#     address3
#     address4 
#     address5
#     locality 
#     administrative_area
#     postal_code
#     country
#     boundaries
#     max_esults
#     search_type
#     extras
#     license_key
#     is_live

from place_search_rest import place_search

single_line = "17 Battery Place, New York, NY 10004"
address1 = ""
address2 = ""
address3 = ""
address4 = ""
address5 = ""
locality = ""
administrative_area = ""
postal_code = ""
country = "USA"
boundaries = ""
max_results = ""
search_type = ""
extras = ""
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the search method.
response = place_search(
    single_line,
    address1,
    address2,
    address3,
    address4,
    address5,
    locality,
    administrative_area,
    postal_code,
    country,
    boundaries,
    max_results,
    search_type,
    extras,
    license_key,
    is_live
)

# 3. Inspect results.
if not response.get('Error'):
    print("\n* Results *\n")

    if response.get('SearchInfo'):
        info = response.get("SearchInfo", {})
            
        print("Search Info:")
        print(f"Status           : {info.get('Status', '')}")
        print(f"NumberOfLocations: {info.get('NumberOfLocations', '')}")
        print(f"Notes            : {info.get('Notes', '')}")
        print(f"NotesDesc        : {info.get('NotesDesc', '')}")
        print(f"Warnings         : {info.get('Warnings', '')}")
        print(f"WarningDesc      : {info.get('WarningDesc', '')}\n")

        if response.get('Locations'):
            locations = response.get("Locations", [])
            for idx, loc in enumerate(locations, start=1):
                print(f"Location #{idx}")
                print(f"\tPrecisionLevel: {loc.get('PrecisionLevel', '')}")
                print(f"\tType          : {loc.get('Type', '')}")
                print(f"\tLatitude      : {loc.get('Latitude', '')}")
                print(f"\tLongitude     : {loc.get('Longitude', '')}")

                if loc.get('AddressComponents'):
                    address = loc.get("AddressComponents", {})
                    if address:
                        print(f"\tPremiseNumber                  : {address.get('PremiseNumber', '')}")
                        print(f"\tThoroughfare                   : {address.get('Thoroughfare', '')}")
                        print(f"\tDoubleDependentLocality        : {address.get('DoubleDependentLocality', '')}")
                        print(f"\tDependentLocality              : {address.get('DependentLocality', '')}")
                        print(f"\tLocality                       : {address.get('Locality', '')}")
                        print(f"\tAdministrativeArea1            : {address.get('AdministrativeArea1', '')}")
                        print(f"\tAdministrativeArea1Abbreviation: {address.get('AdministrativeArea1Abbreviation', '')}")
                        print(f"\tAdministrativeArea2            : {address.get('AdministrativeArea2', '')}")
                        print(f"\tAdministrativeArea2Abbreviation: {address.get('AdministrativeArea2Abbreviation', '')}")
                        print(f"\tAdministrativeArea3            : {address.get('AdministrativeArea3', '')}")
                        print(f"\tAdministrativeArea3Abbreviation: {address.get('AdministrativeArea3Abbreviation', '')}")
                        print(f"\tAdministrativeArea4            : {address.get('AdministrativeArea4', '')}")
                        print(f"\tAdministrativeArea4Abbreviation: {address.get('AdministrativeArea4Abbreviation', '')}")
                        print(f"\tPostalCode                     : {address.get('PostalCode', '')}")
                        print(f"\tCountry                        : {address.get('Country', '')}")
                        print(f"\tCountryISO2                    : {address.get('CountryISO2', '')}")
                        print(f"\tCountryISO3                    : {address.get('CountryISO3', '')}")
                        print(f"\tPlaceName                      : {address.get('PlaceName', '')}")
                        print(f"\tGoogleMapsURL                  : {address.get('GoogleMapsURL', '')}")
                        print(f"\tIsUnincorporated               : {address.get('IsUnincorporated', '')}")
                        print(f"\tStateFIPS                      : {address.get('StateFIPS', '')}")
                        print(f"\tCountyFIPS                     : {address.get('CountyFIPS', '')}")
                        print(f"\tCensusTract                    : {address.get('CensusTract', '')}")
                        print(f"\tCensusBlock                    : {address.get('CensusBlock', '')}")
                        print(f"\tCensusGeoID                    : {address.get('CensusGeoID', '')}")
                        print(f"\tClassFP                        : {address.get('ClassFP', '')}")
                        print(f"\tCongressCode                   : {address.get('CongressCode', '')}")
                        print(f"\tSLDUST                         : {address.get('SLDUST', '')}")
                        print(f"\tSLDLST                         : {address.get('SLDLST', '')}")
                        print(f"\tTimeZone_UTC                   : {address.get('TimeZone_UTC', '')}")
else:
    print("\n* Error *\n")
    err = response.get('Error', {})
    print(f"\tError Type     : {err.get('Type', '')}")
    print(f"\tError Type Code: {err.get('TypeCode', '')}")
    print(f"\tError Desc     : {err.get('Desc', '')}")
    print(f"\tError Desc Code: {err.get('DescCode', '')}")
```

# AGI - ReverseSearch

Retrieve address/location information for a given latitude and longitude anywhere in the world.

### [ReverseSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-reversesearch/)

## Library Usage

```
# 1. Input.
#
# Fields:
#    latitude     
#    longitude    
#    search_radius
#    country      
#    max_results   
#    search_type  
#    license_key
#    is_live

from reverse_search_rest import reverse_search

latitude = "40.6892"
longitude = "-74.0445"
search_radius = "5"
country = "USA"
max_results = "2"
search_type = "All"
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the reverse geocoding method.
response = reverse_search(
    latitude,
    longitude,
    search_radius,
    country,
    max_results,
    search_type,
    license_key,
    is_live
)

# 3. Inspect results.
if not response.get('Error'):
    print("\n* Results *\n")

    if response.get('SearchInfo'):
        info = response.get("SearchInfo", {})
            
        print("Search Info:")
        print(f"Status           : {info.get('Status', '')}")
        print(f"NumberOfLocations: {info.get('NumberOfLocations', '')}")
        print(f"Notes            : {info.get('Notes', '')}")
        print(f"NotesDesc        : {info.get('NotesDesc', '')}")
        print(f"Warnings         : {info.get('Warnings', '')}")
        print(f"WarningDesc      : {info.get('WarningDesc', '')}\n")

        if response.get('Locations'):
            locations = response.get("Locations", [])
            for idx, loc in enumerate(locations, start=1):
                print(f"Location #{idx}")
                print(f"\tPrecisionLevel: {loc.get('PrecisionLevel', '')}")
                print(f"\tType          : {loc.get('Type', '')}")
                print(f"\tLatitude      : {loc.get('Latitude', '')}")
                print(f"\tLongitude     : {loc.get('Longitude', '')}")

                if loc.get('AddressComponents'):
                    address = loc.get("AddressComponents", {})
                    if address:
                        print(f"\tPremiseNumber                  : {address.get('PremiseNumber', '')}")
                        print(f"\tThoroughfare                   : {address.get('Thoroughfare', '')}")
                        print(f"\tDoubleDependentLocality        : {address.get('DoubleDependentLocality', '')}")
                        print(f"\tDependentLocality              : {address.get('DependentLocality', '')}")
                        print(f"\tLocality                       : {address.get('Locality', '')}")
                        print(f"\tAdministrativeArea1            : {address.get('AdministrativeArea1', '')}")
                        print(f"\tAdministrativeArea1Abbreviation: {address.get('AdministrativeArea1Abbreviation', '')}")
                        print(f"\tAdministrativeArea2            : {address.get('AdministrativeArea2', '')}")
                        print(f"\tAdministrativeArea2Abbreviation: {address.get('AdministrativeArea2Abbreviation', '')}")
                        print(f"\tAdministrativeArea3            : {address.get('AdministrativeArea3', '')}")
                        print(f"\tAdministrativeArea3Abbreviation: {address.get('AdministrativeArea3Abbreviation', '')}")
                        print(f"\tAdministrativeArea4            : {address.get('AdministrativeArea4', '')}")
                        print(f"\tAdministrativeArea4Abbreviation: {address.get('AdministrativeArea4Abbreviation', '')}")
                        print(f"\tPostalCode                     : {address.get('PostalCode', '')}")
                        print(f"\tCountry                        : {address.get('Country', '')}")
                        print(f"\tCountryISO2                    : {address.get('CountryISO2', '')}")
                        print(f"\tCountryISO3                    : {address.get('CountryISO3', '')}")
                        print(f"\tPlaceName                      : {address.get('PlaceName', '')}")
                        print(f"\tGoogleMapsURL                  : {address.get('GoogleMapsURL', '')}")
                        print(f"\tIsUnincorporated               : {address.get('IsUnincorporated', '')}")
                        print(f"\tStateFIPS                      : {address.get('StateFIPS', '')}")
                        print(f"\tCountyFIPS                     : {address.get('CountyFIPS', '')}")
                        print(f"\tCensusTract                    : {address.get('CensusTract', '')}")
                        print(f"\tCensusBlock                    : {address.get('CensusBlock', '')}")
                        print(f"\tCensusGeoID                    : {address.get('CensusGeoID', '')}")
                        print(f"\tClassFP                        : {address.get('ClassFP', '')}")
                        print(f"\tCongressCode                   : {address.get('CongressCode', '')}")
                        print(f"\tSLDUST                         : {address.get('SLDUST', '')}")
                        print(f"\tSLDLST                         : {address.get('SLDLST', '')}")
                        print(f"\tTimeZone_UTC                   : {address.get('TimeZone_UTC', '')}")
else:
    print("\n* Error *\n")
    err = response.get('Error', {})
    print(f"\tError Type     : {err.get('Type', '')}")
    print(f"\tError Type Code: {err.get('TypeCode', '')}")
    print(f"\tError Desc     : {err.get('Desc', '')}")
    print(f"\tError Desc Code: {err.get('DescCode', '')}")
```

# PlaceSearch REST SDK Example Usage

A full usage example for the PlaceSearch REST SDK is provided in the `address-geocode-international-python-example/place_search_rest_sdk_example.py` script. This script demonstrates how to:
- Set up input parameters for a PlaceSearch request
- Call the `place_search` function with those parameters
- Display the search results and address components returned by the API
- Handle errors and edge cases in the response

To run the example, execute the script with your license key and desired live/trial mode. The script will print the input parameters, call the API, and display the results in a readable format.

Refer to the script for a complete demonstration of integrating and using the PlaceSearch REST SDK in a Python application.

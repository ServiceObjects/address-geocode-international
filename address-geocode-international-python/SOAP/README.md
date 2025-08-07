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
# 1. Input.
#
# Required fields:
#              license
#              is_live_key
#
# Optional fields:
#              single_line
#              address1 
#              address2
#              address3
#              address4 
#              address5
#              locality 
#              administrative_area
#              postal_code
#              country
#              boundaries
#              max_esults
#              search_type
#              extras

from place_search_soap import PlaceSearch
from helper import response_to_dict

# 2. Call the method.
service = PlaceSearch(license, True, timeout_ms=10000)
response = service.place_search(
    single_line="17 Battery Place, New York, NY 10004",
    address1="",
    address2="",
    address3="",
    address4="",
    address5="",
    locality="",
    administrative_area="",
    postal_code="",
    country="USA",
    boundaries="",
    max_results="",
    search_type="",
    extras=""
)

# 3. Inspect results.
data = response_to_dict(response)
if "Error" not in data:
    print("\n* Results *\n")
            
    print("Search Info:")
    print(f'Status           : {data["SearchInfo"].get("Status", "")}')
    print(f'NumberOfLocations: {data["SearchInfo"].get("NumberOfLocations", "")}')
    print(f'Notes            : {data["SearchInfo"].get("Notes", "")}')
    print(f'NotesDesc        : {data["SearchInfo"].get("NotesDesc", "")}')
    print(f'Warnings         : {data["SearchInfo"].get("Warnings", "")}')
    print(f'WarningDesc      : {data["SearchInfo"].get("WarningDesc", "")}')
    idx = 0;
    for key in data.keys():
        if "Locations" in key:
            results = data[key];
            if results is None or len(results) == 0:
                print(f"Response Key: {key} has no records.");
                continue;
            idx += 1
            print(f"Location #{idx}")
            print(f'\tPrecisionLevel                : {data[key].get("PrecisionLevel", "")}')
            print(f'\tType                          : {data[key].get("Type", "")}')
            print(f'\tLatitude                      : {data[key].get("Latitude", "")}')
            print(f'\tLongitude                     : {data[key].get("Longitude", "")}')
            print(f'\tPremiseNumber                 : {data[key].get("PremiseNumber", "")}')
            print(f'\tThoroughfare                  : {data[key].get("Thoroughfare", "")}')
            print(f'\tDoubleDependentLocality       : {data[key].get("DoubleDependentLocality", "")}')
            print(f'\tDependentLocality             : {data[key].get("DependentLocality", "")}')
            print(f'\tLocality                      : {data[key].get("Locality", "")}')
            print(f'\tAdministrativeArea1           : {data[key].get("AdministrativeArea1", "")}')
            print(f'\AdministrativeArea1Abbreviation: {data[key].get("AdministrativeArea1Abbreviation", "")}')
            print(f'\tAdministrativeArea2           : {data[key].get("AdministrativeArea2", "")}')
            print(f'\AdministrativeArea2Abbreviation: {data[key].get("AdministrativeArea2Abbreviation", "")}')
            print(f'\tAdministrativeArea3           : {data[key].get("AdministrativeArea3", "")}')
            print(f'\AdministrativeArea3Abbreviation: {data[key].get("AdministrativeArea3Abbreviation", "")}')
            print(f'\tAdministrativeArea4           : {data[key].get("AdministrativeArea4", "")}')
            print(f'\AdministrativeArea4Abbreviation: {data[key].get("AdministrativeArea4Abbreviation", "")}')
            print(f'\tPostalCode                    : {data[key].get("PostalCode", "")}')
            print(f'\tCountry                       : {data[key].get("Country", "")}')
            print(f'\tCountryISO2                   : {data[key].get("CountryISO2", "")}')
            print(f'\tCountryISO3                   : {data[key].get("CountryISO3", "")}')
            print(f'\tPlaceName                     : {data[key].get("PlaceName", "")}')
            print(f'\tGoogleMapsURL                 : {data[key].get("GoogleMapsURL", "")}')
            print(f'\tIsUnincorporated              : {data[key].get("IsUnincorporated", "")}')
            print(f'\tStateFIPS                     : {data[key].get("StateFIPS", "")}')
            print(f'\tCountyFIPS                    : {data[key].get("CountyFIPS", "")}')
            print(f'\tCensusTract                   : {data[key].get("CensusTract", "")}')
            print(f'\tCensusBlock                   : {data[key].get("CensusBlock", "")}')
            print(f'\tCensusGeoID                   : {data[key].get("CensusGeoID", "")}')
            print(f'\tClassFP                       : {data[key].get("ClassFP", "")}')
            print(f'\tCongressCode                  : {data[key].get("CongressCode", "")}')
            print(f'\tSLDUST                        : {data[key].get("SLDUST", "")}')
            print(f'\tSLDLST                        : {data[key].get("SLDLST", "")}')
            print(f'\tTimeZoneUTC                   : {data[key].get("TimeZone_UTC", "")}')
else:
    print("\n* Error *\n")
    print(f'Type    : {data["Error"].get("Type", "")}')
    print(f'TypeCode: {data["Error"].get("TypeCode", "")}')
    print(f'Desc    : {data["Error"].get("Desc", "")}')
    print(f'DescCode: {data["Error"].get("DescCode", "")}')
```

# AGI - ReverseSearch

Retrieve address/location information for a given latitude and longitude anywhere in the world.

### [ReverseSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-reversesearch/)

## Library Usage

```
# 1. Input.
#
# Required fields:
#               license
#               is_live_key
#
# Optional fields:
#               latitude     
#               longitude    
#               search_radius
#               country      
#               max_results   
#               search_type  

from reverse_search_soap import ReverseSearch
from helper import response_to_dict

# 2. Call the method.
service = ReverseSearch(license, True, timeout_ms=10000)
response = service.reverse_search(
    latitude="40.6892",
    longitude="-74.0445",
    search_radius="5",
    country="USA",
    max_results="2",
    search_type="All",
)

# 3. Inspect results.
data = response_to_dict(response)
if "Error" not in data:
    print("\n* Results *\n")
            
    print("Search Info:")
    print(f'Status           : {data["SearchInfo"].get("Status", "")}')
    print(f'NumberOfLocations: {data["SearchInfo"].get("NumberOfLocations", "")}')
    print(f'Notes            : {data["SearchInfo"].get("Notes", "")}')
    print(f'NotesDesc        : {data["SearchInfo"].get("NotesDesc", "")}')
    print(f'Warnings         : {data["SearchInfo"].get("Warnings", "")}')
    print(f'WarningDesc      : {data["SearchInfo"].get("WarningDesc", "")}')
    idx = 0;
    for key in data.keys():
        if "Locations" in key:
            results = data[key];
            if results is None or len(results) == 0:
                print(f"Response Key: {key} has no records.");
                continue;
            idx += 1
            print(f"Location #{idx}")
            print(f'\tPrecisionLevel                : {data[key].get("PrecisionLevel", "")}')
            print(f'\tType                          : {data[key].get("Type", "")}')
            print(f'\tLatitude                      : {data[key].get("Latitude", "")}')
            print(f'\tLongitude                     : {data[key].get("Longitude", "")}')
            print(f'\tPremiseNumber                 : {data[key].get("PremiseNumber", "")}')
            print(f'\tThoroughfare                  : {data[key].get("Thoroughfare", "")}')
            print(f'\tDoubleDependentLocality       : {data[key].get("DoubleDependentLocality", "")}')
            print(f'\tDependentLocality             : {data[key].get("DependentLocality", "")}')
            print(f'\tLocality                      : {data[key].get("Locality", "")}')
            print(f'\tAdministrativeArea1           : {data[key].get("AdministrativeArea1", "")}')
            print(f'\AdministrativeArea1Abbreviation: {data[key].get("AdministrativeArea1Abbreviation", "")}')
            print(f'\tAdministrativeArea2           : {data[key].get("AdministrativeArea2", "")}')
            print(f'\AdministrativeArea2Abbreviation: {data[key].get("AdministrativeArea2Abbreviation", "")}')
            print(f'\tAdministrativeArea3           : {data[key].get("AdministrativeArea3", "")}')
            print(f'\AdministrativeArea3Abbreviation: {data[key].get("AdministrativeArea3Abbreviation", "")}')
            print(f'\tAdministrativeArea4           : {data[key].get("AdministrativeArea4", "")}')
            print(f'\AdministrativeArea4Abbreviation: {data[key].get("AdministrativeArea4Abbreviation", "")}')
            print(f'\tPostalCode                    : {data[key].get("PostalCode", "")}')
            print(f'\tCountry                       : {data[key].get("Country", "")}')
            print(f'\tCountryISO2                   : {data[key].get("CountryISO2", "")}')
            print(f'\tCountryISO3                   : {data[key].get("CountryISO3", "")}')
            print(f'\tPlaceName                     : {data[key].get("PlaceName", "")}')
            print(f'\tGoogleMapsURL                 : {data[key].get("GoogleMapsURL", "")}')
            print(f'\tIsUnincorporated              : {data[key].get("IsUnincorporated", "")}')
            print(f'\tStateFIPS                     : {data[key].get("StateFIPS", "")}')
            print(f'\tCountyFIPS                    : {data[key].get("CountyFIPS", "")}')
            print(f'\tCensusTract                   : {data[key].get("CensusTract", "")}')
            print(f'\tCensusBlock                   : {data[key].get("CensusBlock", "")}')
            print(f'\tCensusGeoID                   : {data[key].get("CensusGeoID", "")}')
            print(f'\tClassFP                       : {data[key].get("ClassFP", "")}')
            print(f'\tCongressCode                  : {data[key].get("CongressCode", "")}')
            print(f'\tSLDUST                        : {data[key].get("SLDUST", "")}')
            print(f'\tSLDLST                        : {data[key].get("SLDLST", "")}')
            print(f'\tTimeZoneUTC                   : {data[key].get("TimeZone_UTC", "")}')
else:
    print("\n* Error *\n")
    print(f'Type    : {data["Error"].get("Type", "")}')
    print(f'TypeCode: {data["Error"].get("TypeCode", "")}')
    print(f'Desc    : {data["Error"].get("Desc", "")}')
    print(f'DescCode: {data["Error"].get("DescCode", "")}')
```
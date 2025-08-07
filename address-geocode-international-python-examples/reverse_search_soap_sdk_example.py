import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-international-python/SOAP"))

from reverse_search_soap import ReverseSearch
from helper import response_to_dict

def reverse_search_soap_sdk_go(license, is_live_key):
    print("\n" + "-" * 56)
    print("Address Geocode International - ReverseSearch - SOAP SDK")
    print("-" * 56)

    print("* Input *\n")
    print(f"Latitude     : 40.6892")
    print(f"Longitude    : -74.0445")
    print(f"Search Radius: 5")
    print(f"Country      : USA")
    print(f"MaxResults   : 2")
    print(f"Search Type  : All")
    print(f"Is Live      : {is_live_key}")
    print(f"LicenseKey   : {license}")

    try:
        service = ReverseSearch(license, is_live_key, timeout_ms=10000)
        response = service.reverse_search(
            latitude="40.6892",
            longitude="-74.0445",
            search_radius="5",
            country="USA",
            max_results="2",
            search_type="All",
        )

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
    except Exception as ex:
        print("\nError calling service:", ex)

import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-international-python/SOAP"))

from place_search_soap import PlaceSearch
from helper import response_to_dict

def place_search_soap_sdk_go(license, is_live_key):
    print("\n" + "-" * 54)
    print("Address Geocode International - PlaceSearch - SOAP SDK")
    print("-" * 54)

    print("* Input *\n")
    print(f"SingleLine         : 17 Battery Place, New York, NY 10004")
    print(f"Address1           : ")
    print(f"Address2           : ")
    print(f"Address3           : ")
    print(f"Address4           : ")
    print(f"Address5           : ")
    print(f"Locality           : ")
    print(f"Administrative Area: ")
    print(f"Postal Code        : ")
    print(f"Country            : USA")
    print(f"Boundaries         : ")
    print(f"Max Results        : ")
    print(f"Search Type        : ")
    print(f"Extras             : ")
    print(f"Is Live            : {is_live_key}")
    print(f"License Key        : {license}")

    try:
        service = PlaceSearch(license, is_live_key, timeout_ms=10000)
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

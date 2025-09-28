import sys
import os

# Add REST client path for module import
sys.path.insert(0, os.path.abspath("../address-geocode-international-python/REST"))

from place_search_rest import place_search

def place_search_rest_sdk_go(license_key, is_live):
    print("\n" + "-" * 54)
    print("Address Geocode International - PlaceSearch - REST SDK")
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
    print(f"Is Live            : {is_live}")
    print(f"License Key        : {license_key}")

    try:
        response = place_search(
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
            extras="",
            license_key=license_key,
            is_live=is_live
        )

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
    except RuntimeError as ex:
        print("\nError calling service:", ex)

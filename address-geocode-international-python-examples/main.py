from place_search_rest_sdk_example import place_search_rest_sdk_go
from reverse_search_rest_sdk_example import reverse_search_rest_sdk_go
from place_search_soap_sdk_example import place_search_soap_sdk_go
from reverse_search_soap_sdk_example import reverse_search_soap_sdk_go

if __name__ == "__main__":

    # Your license key from Service Objects.
    # Trial license keys will only work on the
    # trail environments and production license
    # keys will only owork on production environments.
    license_key = "LICENSE KEY"

    is_live_license_key = False

    #  Address Geocode – International - PlaceSearch - REST SDK
    place_search_rest_sdk_go(license_key, is_live_license_key)

    # Address Geocode – International - PlaceSearch - SOAP SDK
    place_search_soap_sdk_go(license_key, is_live_license_key)

    # Address Geocode – International - ReverseSearch - REST SDK
    reverse_search_rest_sdk_go(license_key, is_live_license_key)

    # Address Geocode – International - ReverseSearch - SOAP SDK
    reverse_search_soap_sdk_go(license_key, is_live_license_key)

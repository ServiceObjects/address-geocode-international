// See https://aka.ms/new-console-template for more information
using address_geocode_international_dot_net_examples;

//Your license key from Service Objects.
//Trial license keys will only work on the
//trail environments and production license
//keys will only owork on production environments.
string LicenseKey = "LICENSE KEY";

bool IsProductionKey = false;

// Address Geocode – International - PlaceSearch - REST SDK
PlaceSearchRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – International - PlaceSearch - SOAP SDK
PlaceSearchSoapSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – International - ReverseSearch - REST SDK
ReverseSearchRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – International - ReverseSearch - SOAP SDK
ReverseSearchSoapSdkExample.Go(LicenseKey, IsProductionKey);

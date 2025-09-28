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
// 1. Build the input.
//
// Fields:
//     SingleLine
//     Address1
//     Address2
//     Address3
//     Address4
//     Address5
//     Locality
//     AdministrativeArea
//     PostalCode
//     Country
//     Boundaries
//     MaxResults
//     SearchType
//     Extras
//     IsLive
//     LicenseKey

using address_geocode_international_dot_net.REST;

string SingleLine= "17 Battery Place, New York, NY 10004",
string Address1 = "",
string Address2 = "",
string Address3 = "",
string Address4 = "",
string Address5 = "",
string Locality = "",
string AdministrativeArea = "",
string PostalCode = "",
string Country = "US",
string Boundaries = "",
string MaxResults = "2",
string SearchType = "Address",
string Extras = "",
int TimeoutSeconds = 15
bool IsLive = true,
string LicenseKey = "YOUR LICENSE KEY",

PlaceSearchClient.PlaceSearchInput placeSearchInput = new(
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
                LicenseKey,
                IsLive,
                TimeoutSeconds
            );

// 2. Call the sync Invoke() method.
AGIPlaceSearchResponse response = PlaceSearchClient.Invoke(placeSearchInput);

// 3. Inspect results.
if (response.Error is null)
{
    Console.WriteLine("\r\n* Validation *\r\n");

    if (response.SearchInfo != null)
    {
        Console.WriteLine("Search Info:");
        Console.WriteLine($"Status           : {response.SearchInfo.Status}");
        Console.WriteLine($"NumberOfLocations: {response.SearchInfo.NumberOfLocations}");
        Console.WriteLine($"Notes            : {response.SearchInfo.Notes}");
        Console.WriteLine($"NotesDesc        : {response.SearchInfo.NotesDesc}");
        Console.WriteLine();
    }

    if (response.Locations != null && response.Locations.Length > 0)
    {
        int count = 1;
        foreach (var location in response.Locations)
        {
            Console.WriteLine($"Location #{count++}");
            Console.WriteLine($"Precision Level: {location.PrecisionLevel}");
            Console.WriteLine($"Type           : {location.Type}");
            Console.WriteLine($"Latitude       : {location.Latitude}");
            Console.WriteLine($"Longitude      : {location.Longitude}");

            if (location.AddressComponents != null)
            {
                Console.WriteLine("Address Components:");
                Console.WriteLine($"Country          : {location.AddressComponents.Country}");
                Console.WriteLine($"CountryISO2      : {location.AddressComponents.CountryISO2}");
                Console.WriteLine($"CountryISO3      : {location.AddressComponents.CountryISO3}");
                Console.WriteLine($"TimeZone (UTC)   : {location.AddressComponents.TimeZone_UTC}");
            }
                                                
        }
    }
    else
    {
        Console.WriteLine("No locations found.");
    }
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");

    Console.WriteLine($"Error Type     : {response.Error.Type}");
    Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
    Console.WriteLine($"Error Desc     : {response.Error.Desc}");
    Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
}
```

# AGI - ReverseSearch

Retrieve address/location information for a given latitude and longitude anywhere in the world.

### [ReverseSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-reversesearch/)

## Library Usage

```
// 1. Build the input.
//
//  Fields:
//              Latitude
//              Longitude
//              SearchRadius
//              Country
//              MaxResults
//              SearchType
//              LicenseKey
//              IsLive
//              TimeoutSeconds

using address_geocode_international_dot_net.REST;

ReverseSearchInput reverseSearchInput = new(
                Latitude: "40.748369",
                Longitude: "-73.984853",
                SearchRadius: "5",
                Country: "US",
                MaxResults: "2",
                SearchType: "All",
                LicenseKey: licenseKey,
                IsLive: true,
                TimeoutSeconds: 15
            );
// 2. Call the sync Invoke() method.
AGIReverseSearchResponse response = ReverseSearchClient.Invoke(reverseSearchInput);

// 3. Inspect results.
 if (response.Error is null)
{
    Console.WriteLine("\r\n* Results *\r\n");

    if (response.SearchInfo != null)
    {
        Console.WriteLine("Search Info:");
        Console.WriteLine($"Status             : {response.SearchInfo.Status}");
        Console.WriteLine($"Number Of Locations: {response.SearchInfo.NumberOfLocations}");
        Console.WriteLine($"Notes              : {response.SearchInfo.Notes}");
        Console.WriteLine($"Notes Desc         : {response.SearchInfo.NotesDesc}");
        Console.WriteLine();
    }

    if (response.Locations != null && response.Locations.Count > 0)
    {
        int index = 1;
        foreach (var location in response.Locations)
        {
            Console.WriteLine($"Location #{index++}");
            Console.WriteLine($"Precision Level: {location.PrecisionLevel}");
            Console.WriteLine($"Type           : {location.Type}");
            Console.WriteLine($"Latitude       : {location.Latitude}");
            Console.WriteLine($"Longitude      : {location.Longitude}");

            Console.WriteLine("Address Components:");
            Console.WriteLine($"Country          : {location.AddressComponents?.Country}");
            Console.WriteLine($"Country ISO2     : {location.AddressComponents?.CountryISO2}");
            Console.WriteLine($"Country ISO3     : {location.AddressComponents?.CountryISO3}");
            Console.WriteLine($"Locality         : {location.AddressComponents?.Locality}");
            Console.WriteLine($"Postal Code      : {location.AddressComponents?.PostalCode}");
            Console.WriteLine($"TimeZone (UTC)   : {location.AddressComponents?.TimeZone_UTC}");
                                               
        }
    }
    else
    {
        Console.WriteLine("No locations found.");
    }
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");

    Console.WriteLine($"Error Type     : {response.Error.Type}");
    Console.WriteLine($"Error Type Code: {response.Error.TypeCode}");
    Console.WriteLine($"Error Desc     : {response.Error.Desc}");
    Console.WriteLine($"Error Desc Code: {response.Error.DescCode}");
}
```

---
using address_geocode_international_dot_net.REST;

namespace address_geocode_international_dot_net_examples
{
    internal static class PlaceSearchRestSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n------------------------------------------------------");
            Console.WriteLine("Address Geocode International - PlaceSearch - REST SDK");
            Console.WriteLine("------------------------------------------------------");

            // Construct input object
            PlaceSearchClient.PlaceSearchInput placeSearchInput = new(
                SingleLine: "17 Battery Place, New York, NY 10004",
                Address1: "",
                Address2: "",
                Address3: "",
                Address4: "",
                Address5: "",
                Locality: "",
                AdministrativeArea: "",
                PostalCode: "",
                Country: "US",
                Boundaries: "",
                MaxResults: "2",
                SearchType: "Address",
                Extras: "",
                LicenseKey: licenseKey,
                IsLive: isLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Single Line       : {placeSearchInput.SingleLine}");
            Console.WriteLine($"Address1          : {placeSearchInput.Address1}");
            Console.WriteLine($"Address2          : {placeSearchInput.Address2}");
            Console.WriteLine($"Address3          : {placeSearchInput.Address3}");
            Console.WriteLine($"Address4          : {placeSearchInput.Address4}");
            Console.WriteLine($"Address5          : {placeSearchInput.Address5}");
            Console.WriteLine($"Locality          : {placeSearchInput.Locality}");
            Console.WriteLine($"AdministrativeArea: {placeSearchInput.AdministrativeArea}");
            Console.WriteLine($"PostalCode        : {placeSearchInput.PostalCode}");
            Console.WriteLine($"Country           : {placeSearchInput.Country}");
            Console.WriteLine($"Boundaries        : {placeSearchInput.Boundaries}");
            Console.WriteLine($"MaxResults        : {placeSearchInput.MaxResults}");
            Console.WriteLine($"SearchType        : {placeSearchInput.SearchType}");
            Console.WriteLine($"Extras            : {placeSearchInput.Extras}");
            Console.WriteLine($"Is Live           : {placeSearchInput.IsLive}");
            Console.WriteLine($"License Key       : {placeSearchInput.LicenseKey}");
            Console.WriteLine($"Timeout (sec)     : {placeSearchInput.TimeoutSeconds}");

            // Invoke REST API
            AGIPlaceSearchResponse response = PlaceSearchClient.Invoke(placeSearchInput);

            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Results *\r\n");

                if (response.SearchInfo != null)
                {
                    Console.WriteLine("Search Info:");
                    Console.WriteLine($"Status           : {response.SearchInfo.Status}");
                    Console.WriteLine($"NumberOfLocations: {response.SearchInfo.NumberOfLocations}");
                    Console.WriteLine($"Notes            : {response.SearchInfo.Notes}");
                    Console.WriteLine($"NotesDesc        : {response.SearchInfo.NotesDesc}");
                    Console.WriteLine($"Warnings         : {response.SearchInfo.Warnings}");
                    Console.WriteLine($"WarningDesc      : {response.SearchInfo.WarningDesc}");

                    Console.WriteLine();
                }

                if (response.Locations != null && response.Locations.Length > 0)
                {
                    int count = 1;
                    foreach (var location in response.Locations)
                    {
                        Console.WriteLine($"\r\nLocation #{count++}\r\n");
                        Console.WriteLine($"\tPrecision Level: {location.PrecisionLevel}");
                        Console.WriteLine($"\tType           : {location.Type}");
                        Console.WriteLine($"\tLatitude       : {location.Latitude}");
                        Console.WriteLine($"\tLongitude      : {location.Longitude}");

                        if (location.AddressComponents != null)
                        {
                            Console.WriteLine($"\tPremiseNumber                  : {location.AddressComponents.PremiseNumber}");
                            Console.WriteLine($"\tThoroughfare                   : {location.AddressComponents.Thoroughfare}");
                            Console.WriteLine($"\tDoubleDependentLocality        : {location.AddressComponents.DoubleDependentLocality}");
                            Console.WriteLine($"\tDependentLocality              : {location.AddressComponents.DependentLocality}");
                            Console.WriteLine($"\tLocality                       : {location.AddressComponents.Locality}");
                            Console.WriteLine($"\tAdministrativeArea1            : {location.AddressComponents.AdministrativeArea1}");
                            Console.WriteLine($"\tAdministrativeArea1Abbreviation: {location.AddressComponents.AdministrativeArea1Abbreviation}");
                            Console.WriteLine($"\tAdministrativeArea2            : {location.AddressComponents.AdministrativeArea2}");
                            Console.WriteLine($"\tAdministrativeArea2Abbreviation: {location.AddressComponents.AdministrativeArea2Abbreviation}");
                            Console.WriteLine($"\tAdministrativeArea3            : {location.AddressComponents.AdministrativeArea3}");
                            Console.WriteLine($"\tAdministrativeArea3Abbreviation: {location.AddressComponents.AdministrativeArea3Abbreviation}");
                            Console.WriteLine($"\tAdministrativeArea4            : {location.AddressComponents.AdministrativeArea4}");
                            Console.WriteLine($"\tAdministrativeArea4Abbreviation: {location.AddressComponents.AdministrativeArea4Abbreviation}");
                            Console.WriteLine($"\tPostalCode                     : {location.AddressComponents.PostalCode}");
                            Console.WriteLine($"\tCountry                        : {location.AddressComponents.Country}");
                            Console.WriteLine($"\tCountryISO2                    : {location.AddressComponents.CountryISO2}");
                            Console.WriteLine($"\tCountryISO3                    : {location.AddressComponents.CountryISO3}");
                            Console.WriteLine($"\tPlaceName                      : {location.AddressComponents.PlaceName}");
                            Console.WriteLine($"\tGoogleMapsURL                  : {location.AddressComponents.GoogleMapsURL}");
                            Console.WriteLine($"\tIsUnincorporated               : {location.AddressComponents.IsUnincorporated}");
                            Console.WriteLine($"\tStateFIPS                      : {location.AddressComponents.StateFIPS}");
                            Console.WriteLine($"\tCountyFIPS                     : {location.AddressComponents.CountyFIPS}");
                            Console.WriteLine($"\tCensusTract                    : {location.AddressComponents.CensusTract}");
                            Console.WriteLine($"\tCensusBlock                    : {location.AddressComponents.CensusBlock}");
                            Console.WriteLine($"\tCensusGeoID                    : {location.AddressComponents.CensusGeoID}");
                            Console.WriteLine($"\tClassFP                        : {location.AddressComponents.ClassFP}");
                            Console.WriteLine($"\tCongressCode                   : {location.AddressComponents.CongressCode}");
                            Console.WriteLine($"\tSLDUST                         : {location.AddressComponents.SLDUST}");
                            Console.WriteLine($"\tSLDLST                         : {location.AddressComponents.SLDLST}");
                            Console.WriteLine($"\tTimezone_UTC                   : {location.AddressComponents.TimeZone_UTC}");
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
        }
    }
}

using address_geocode_international_dot_net.REST;

namespace address_geocode_international_dot_net_examples
{
    internal static class ReverseSearchRestSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------------");
            Console.WriteLine("Address Geocode International - ReverseSearch - REST SDK");
            Console.WriteLine("--------------------------------------------------------");

            ReverseSearchClient.ReverseSearchInput reverseSearchInput = new(
                Latitude: "40.748369",
                Longitude: "-73.984853",
                SearchRadius: "5",
                Country: "US",
                MaxResults: "2",
                SearchType: "All",
                LicenseKey: licenseKey,
                IsLive: isLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Latitude    : {reverseSearchInput.Latitude}");
            Console.WriteLine($"Longitude   : {reverseSearchInput.Longitude}");
            Console.WriteLine($"SearchRadius: {reverseSearchInput.SearchRadius}");
            Console.WriteLine($"Country     : {reverseSearchInput.Country}");
            Console.WriteLine($"MaxResults  : {reverseSearchInput.MaxResults}");
            Console.WriteLine($"Search Type : {reverseSearchInput.SearchType}");
            Console.WriteLine($"Is Live     : {reverseSearchInput.IsLive}");
            Console.WriteLine($"License Key : {reverseSearchInput.LicenseKey}");
            Console.WriteLine($"Timeout     : {reverseSearchInput.TimeoutSeconds}");

            AGIReverseSearchResponse response = ReverseSearchClient.Invoke(reverseSearchInput);

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
                    Console.WriteLine($"Warnings           : {response.SearchInfo.Warnings}");
                    Console.WriteLine($"WarningDesc        : {response.SearchInfo.WarningDesc}");
                    Console.WriteLine();
                }

                if (response.Locations != null && response.Locations.Count > 0)
                {
                    int index = 1;
                    foreach (var location in response.Locations)
                    {
                        Console.WriteLine($"\r\nLocation #{index++}\r\n");
                        Console.WriteLine($"\tPrecision Level                : {location.PrecisionLevel}");
                        Console.WriteLine($"\tType                           : {location.Type}");
                        Console.WriteLine($"\tLatitude                       : {location.Latitude}");
                        Console.WriteLine($"\tLongitude                      : {location.Longitude}");

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

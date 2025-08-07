using address_geocode_international_dot_net.SOAP;

namespace address_geocode_international_dot_net_examples
{
    internal static class PlaceSearchSoapSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n------------------------------------------------------");
            Console.WriteLine("Address Geocode International - PlaceSearch - SOAP SDK");
            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"SingleLine        : 350 5th Ave, New York, NY 10118");
            Console.WriteLine($"Address1          : ");
            Console.WriteLine($"Address2          : ");
            Console.WriteLine($"Address3          : ");
            Console.WriteLine($"Address4          : ");
            Console.WriteLine($"Address5          : ");
            Console.WriteLine($"Locality          : ");
            Console.WriteLine($"AdministrativeArea: ");
            Console.WriteLine($"PostalCode        : ");
            Console.WriteLine($"Country           : US");
            Console.WriteLine($"Boundaries        : ");
            Console.WriteLine($"MaxResults        : 2");
            Console.WriteLine($"SearchType        : PostalCode");
            Console.WriteLine($"Extras            : ");
            Console.WriteLine($"Is Live           : {isLive.ToString()}");
            Console.WriteLine($"LicenseKey        : {licenseKey}");

            PlaceSearchValidation placeSearchValidation = new(isLive);

            AGIService.ResponseObject response = placeSearchValidation.PlaceSearch(
                SingleLine: "350 5th Ave, New York, NY 10118",
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
                LicenseKey: licenseKey
             ).GetAwaiter().GetResult();

            if (!response.ContainsKey("Error"))
            {

                Console.WriteLine("\r\n* Results *\r\n");

                // Print SearchInfo essentials
                if (response.TryGetValue("SearchInfo", out var searchInfoResults) && searchInfoResults.Count > 0)
                {
                    var searchInfo = searchInfoResults[0];
                    Console.WriteLine("\r\nSearch Info:");
                    Console.WriteLine($"Status           : {(searchInfo.ContainsKey("Status") ? searchInfo["Status"] : "")}");
                    Console.WriteLine($"NumberOfLocations: {(searchInfo.ContainsKey("NumberOfLocations") ? searchInfo["NumberOfLocations"] : "")}");
                    Console.WriteLine($"Notes            : {(searchInfo.ContainsKey("Notes") ? searchInfo["Notes"] : "")}");
                    Console.WriteLine($"NotesDesc        : {(searchInfo.ContainsKey("NotesDesc") ? searchInfo["NotesDesc"] : "")}");
                    Console.WriteLine($"Warnings         : {(searchInfo.ContainsKey("Warnings") ? searchInfo["Warnings"] : "")}");
                    Console.WriteLine($"WarningDesc      : {(searchInfo.ContainsKey("WarningDesc") ? searchInfo["WarningDesc"] : "")}");

                    Console.WriteLine();
                }

                // Print locations with essential fields only
                int locationNumber = 1;
                foreach (string key in response.Keys)
                {
                    if (key.Contains("Locations"))
                    {
                        var results = response[key];
                        if (results == null || results.Count == 0)
                        {
                            Console.WriteLine($"Response Key: {key} has no records.");
                            continue;
                        }

                        foreach (var location in results)
                        {
                            Console.WriteLine("\r\nLocation #" + locationNumber++ + "\r\n");
                            Console.WriteLine("\tPrecisionLevel                 : " + (location.ContainsKey("PrecisionLevel") ? location["PrecisionLevel"] : ""));
                            Console.WriteLine("\tType                           : " + (location.ContainsKey("Type") ? location["Type"] : ""));
                            Console.WriteLine("\tLatitude                       : " + (location.ContainsKey("Latitude") ? location["Latitude"] : ""));
                            Console.WriteLine("\tLongitude                      : " + (location.ContainsKey("Longitude") ? location["Longitude"] : ""));
                            Console.WriteLine("\tPremiseNumber                  : " + (location.ContainsKey("PremiseNumber") ? location["PremiseNumber"] : ""));
                            Console.WriteLine("\tThoroughfare                   : " + (location.ContainsKey("Thoroughfare") ? location["Thoroughfare"] : ""));
                            Console.WriteLine("\tDoubleDependentLocality        : " + (location.ContainsKey("DoubleDependentLocality") ? location["DoubleDependentLocality"] : ""));
                            Console.WriteLine("\tDependentLocality              : " + (location.ContainsKey("DependentLocality") ? location["DependentLocality"] : ""));
                            Console.WriteLine("\tLocality                       : " + (location.ContainsKey("Locality") ? location["Locality"] : ""));
                            Console.WriteLine("\tAdministrativeArea1            : " + (location.ContainsKey("AdministrativeArea1") ? location["AdministrativeArea1"] : ""));
                            Console.WriteLine("\tAdministrativeArea1Abbreviation: " + (location.ContainsKey("AdministrativeArea1Abbreviation") ? location["AdministrativeArea1Abbreviation"] : ""));
                            Console.WriteLine("\tAdministrativeArea2            : " + (location.ContainsKey("AdministrativeArea2") ? location["AdministrativeArea2"] : ""));
                            Console.WriteLine("\tAdministrativeArea2Abbreviation: " + (location.ContainsKey("AdministrativeArea2Abbreviation") ? location["AdministrativeArea2Abbreviation"] : ""));
                            Console.WriteLine("\tAdministrativeArea3            : " + (location.ContainsKey("AdministrativeArea3") ? location["AdministrativeArea3"] : ""));
                            Console.WriteLine("\tAdministrativeArea3Abbreviation: " + (location.ContainsKey("AdministrativeArea3Abbreviation") ? location["AdministrativeArea3Abbreviation"] : ""));
                            Console.WriteLine("\tAdministrativeArea4            : " + (location.ContainsKey("AdministrativeArea4") ? location["AdministrativeArea4"] : ""));
                            Console.WriteLine("\tAdministrativeArea4Abbreviation: " + (location.ContainsKey("AdministrativeArea4Abbreviation") ? location["AdministrativeArea4Abbreviation"] : ""));
                            Console.WriteLine("\tPostalCode                     : " + (location.ContainsKey("PostalCode") ? location["PostalCode"] : ""));
                            Console.WriteLine("\tCountry                        : " + (location.ContainsKey("Country") ? location["Country"] : ""));
                            Console.WriteLine("\tCountryISO2                    : " + (location.ContainsKey("CountryISO2") ? location["CountryISO2"] : ""));
                            Console.WriteLine("\tCountryISO3                    : " + (location.ContainsKey("CountryISO3") ? location["CountryISO3"] : ""));
                            Console.WriteLine("\tPlaceName                      : " + (location.ContainsKey("PlaceName") ? location["PlaceName"] : ""));
                            Console.WriteLine("\tGoogleMapsURL                  : " + (location.ContainsKey("GoogleMapsURL") ? location["GoogleMapsURL"] : ""));
                            Console.WriteLine("\tIsUnincorporated               : " + (location.ContainsKey("IsUnincorporated") ? location["IsUnincorporated"] : ""));
                            Console.WriteLine("\tStateFIPS                      : " + (location.ContainsKey("StateFIPS") ? location["StateFIPS"] : ""));
                            Console.WriteLine("\tCountyFIPS                     : " + (location.ContainsKey("CountyFIPS") ? location["CountyFIPS"] : ""));
                            Console.WriteLine("\tCensusTract                    : " + (location.ContainsKey("CensusTract") ? location["CensusTract"] : ""));
                            Console.WriteLine("\tCensusBlock                    : " + (location.ContainsKey("CensusBlock") ? location["CensusBlock"] : ""));
                            Console.WriteLine("\tCensusGeoID                    : " + (location.ContainsKey("CensusGeoID") ? location["CensusGeoID"] : ""));
                            Console.WriteLine("\tClassFP                        : " + (location.ContainsKey("ClassFP") ? location["ClassFP"] : ""));
                            Console.WriteLine("\tCongressCode                   : " + (location.ContainsKey("CongressCode") ? location["CongressCode"] : ""));
                            Console.WriteLine("\tSLDUST                         : " + (location.ContainsKey("SLDUST") ? location["SLDUST"] : ""));
                            Console.WriteLine("\tSLDLST                         : " + (location.ContainsKey("SLDLST") ? location["SLDLST"] : ""));
                            Console.WriteLine("\tTimezone_UTC                   : " + (location.ContainsKey("TimeZone_UTC") ? location["TimeZone_UTC"] : ""));
                        }
                    }
                }

            }
            else
            {
                // Print only primary error fields
                if (response.TryGetValue("Error", out var errorResults) && errorResults.Count > 0)
                {
                    var error = errorResults[0];
                    Console.WriteLine("\r\n* Error *\r\n");
                    Console.WriteLine($"Type: {(error.ContainsKey("Type") ? error["Type"] : "")}");
                    Console.WriteLine($"TypeCode: {(error.ContainsKey("TypeCode") ? error["TypeCode"] : "")}");
                    Console.WriteLine($"Desc: {(error.ContainsKey("Desc") ? error["Desc"] : "")}");
                    Console.WriteLine($"DescCode: {(error.ContainsKey("DescCode") ? error["DescCode"] : "")}");
                }
                else
                {
                    Console.WriteLine("Unknown error occurred.");
                }
            }
        }
    }
}

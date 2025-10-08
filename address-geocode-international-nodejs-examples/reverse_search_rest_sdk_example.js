import { ReverseSearchClient } from '../address-geocode-international/REST/reverse_search_rest.js';

async function ReverseSearchRestGo(licenseKey, isLive) {
    console.log("\n--------------------------------------------------------");
    console.log("Address Geocode International - ReverseSearch - REST SDK");
    console.log("--------------------------------------------------------");

    const latitude = "34.02984234473112";
    const longitude = "-118.26876271804397"; 
    const searchRadius = "";
    const country = "";
    const maxResults = "";
    const searchType = "BestMatch";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Latitude         : ${latitude}`);
    console.log(`Longitude        : ${longitude}`);
    console.log(`SearchRadius     : ${searchRadius}`);
    console.log(`Country          : ${country}`);
    console.log(`MaxResults       : ${maxResults}`);
    console.log(`SearchType       : ${searchType}`);
    console.log(`License Key      : ${licenseKey}`);
    console.log(`Is Live          : ${isLive}`);
    console.log(`Timeout Seconds  : ${timeoutSeconds}`);

    try {
        const response = await ReverseSearchClient.invoke(
            latitude,
            longitude,
            searchRadius,
            country,
            maxResults,
            searchType,
            licenseKey,
            isLive,
            timeoutSeconds
        );

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Type    : ${response.Error.Type}`);
            console.log(`Error TypeCode: ${response.Error.TypeCode}`);
            console.log(`Error Desc    : ${response.Error.Desc}`);
            console.log(`Error DescCode: ${response.Error.DescCode}`);
            return;
        }

        console.log("\n* Search Info *\n");
        if (response && response.SearchInfo) {
            console.log(`Status           : ${response.SearchInfo.Status}`);
            console.log(`NumberOfLocations: ${response.SearchInfo.NumberOfLocations}`);
            console.log(`Notes            : ${response.SearchInfo.Notes}`);
            console.log(`NotesDesc        : ${response.SearchInfo.NotesDesc}`);
            console.log(`Warnings         : ${response.SearchInfo.Warnings}`);
            console.log(`WarningDesc      : ${response.SearchInfo.WarningDesc}`);
        } else {
            console.log("No search info found.");
        }

        console.log("\n* Locations *\n");
        if (response && response.Locations && response.Locations.length > 0) {
            response.Locations.forEach((location) => {
                console.log(`PrecisionLevel   : ${location.PrecisionLevel}`);
                console.log(`Type             : ${location.Type}`);
                console.log(`Latitude         : ${location.Latitude}`);
                console.log(`Longitude        : ${location.Longitude}`);

                console.log("\n* Address Components *");
                if (location.AddressComponents) {
                    console.log(`PremiseNumber           : ${location.AddressComponents.PremiseNumber}`);
                    console.log(`Thoroughfare            : ${location.AddressComponents.Thoroughfare}`);
                    console.log(`DoubleDependentLocality : ${location.AddressComponents.DoubleDependentLocality}`);
                    console.log(`DependentLocality       : ${location.AddressComponents.DependentLocality}`);
                    console.log(`Locality                : ${location.AddressComponents.Locality}`);
                    console.log(`AdministrativeArea1     : ${location.AddressComponents.AdministrativeArea1}`);
                    console.log(`AdministrativeArea1Abbr : ${location.AddressComponents.AdministrativeArea1Abbreviation}`);
                    console.log(`AdministrativeArea2     : ${location.AddressComponents.AdministrativeArea2}`);
                    console.log(`AdministrativeArea2Abbr : ${location.AddressComponents.AdministrativeArea2Abbreviation}`);
                    console.log(`AdministrativeArea3     : ${location.AddressComponents.AdministrativeArea3}`);
                    console.log(`AdministrativeArea3Abbr : ${location.AddressComponents.AdministrativeArea3Abbreviation}`);
                    console.log(`AdministrativeArea4     : ${location.AddressComponents.AdministrativeArea4}`);
                    console.log(`AdministrativeArea4Abbr : ${location.AddressComponents.AdministrativeArea4Abbreviation}`);
                    console.log(`PostalCode              : ${location.AddressComponents.PostalCode}`);
                    console.log(`Country                 : ${location.AddressComponents.Country}`);
                    console.log(`CountryISO2             : ${location.AddressComponents.CountryISO2}`);
                    console.log(`CountryISO3             : ${location.AddressComponents.CountryISO3}`);
                    console.log(`GoogleMapsURL           : ${location.AddressComponents.GoogleMapsURL}`);
                    console.log(`PlaceName               : ${location.AddressComponents.PlaceName}`);
                    console.log(`IsUnincorporated        : ${location.AddressComponents.IsUnincorporated}`);
                    console.log(`StateFIPS               : ${location.AddressComponents.StateFIPS}`);
                    console.log(`CountyFIPS              : ${location.AddressComponents.CountyFIPS}`);
                    console.log(`CensusTract             : ${location.AddressComponents.CensusTract}`);
                    console.log(`CensusBlock             : ${location.AddressComponents.CensusBlock}`);
                    console.log(`CensusGeoID             : ${location.AddressComponents.CensusGeoID}`);
                    console.log(`ClassFP                 : ${location.AddressComponents.ClassFP}`);
                    console.log(`CongressCode            : ${location.AddressComponents.CongressCode}`);
                    console.log(`SLDUST                  : ${location.AddressComponents.SLDUST}`);
                    console.log(`SLDLST                  : ${location.AddressComponents.SLDLST}`);
                    console.log(`Timezone_UTC            : ${location.AddressComponents.Timezone_UTC}`);
                } else {
                    console.log("No address components found.");
                }
            });
        } else {
            console.log("No locations found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { ReverseSearchRestGo };
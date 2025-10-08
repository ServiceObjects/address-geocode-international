import { ReverseSearchSoap } from '../address-geocode-international/SOAP/reverse_search_soap.js';

async function ReverseSearchSoapGo(licenseKey, isLive) {
    console.log("\n--------------------------------------------------------");
    console.log("Address Geocode International - ReverseSearch - SOAP SDK");
    console.log("--------------------------------------------------------");

    const latitude = "34.02984234473112";
    const longitude = "-118.26876271804397";
    const searchRadius = "";
    const country = "";
    const maxResults = "";
    const searchType = "BestMatch";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Latitude       : ${latitude}`);
    console.log(`Longitude      : ${longitude}`);
    console.log(`SearchRadius   : ${searchRadius}`);
    console.log(`Country        : ${country}`);
    console.log(`MaxResults     : ${maxResults}`);
    console.log(`SearchType     : ${searchType}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const agi = new ReverseSearchSoap(
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
        const response = await agi.invokeAsync();

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Type    : ${response.Error.Type}`);
            console.log(`Error TypeCode: ${response.Error.TypeCode}`);
            console.log(`Error Desc    : ${response.Error.Desc}`);
            console.log(`Error DescCode: ${response.Error.DescCode}`);
            return;
        }

        console.log("\n* Search Info *\n");
        const fields = response?.Response?.[0]?.Value?.Result?.[0]?.Field || [];
        const getValue = (key) => {
            const field = fields.find(f => f.Key === key);
            return field ? field.Value : "";
        };

        console.log(`Status           : ${getValue("Status")}`);
        console.log(`NumberOfLocations: ${getValue("NumberOfLocations")}`);
        console.log(`Notes            : ${getValue("Notes")}`);
        console.log(`NotesDesc        : ${getValue("NotesDesc")}`);
        console.log(`Warnings         : ${getValue("Warnings")}`);
        console.log(`WarningDesc      : ${getValue("WarningDesc")}`);

        console.log("\n* Locations *\n");

        const locationResponses = response?.Response?.filter(r => r.Key.startsWith("Locations")) || [];

        if (locationResponses.length === 0) {
            console.log("No locations found.");
            return;
        }

        locationResponses.forEach((locationResponse, index) => {
            console.log(`\nLocation #${index + 1}`);
            const locFields = locationResponse?.Value?.Result?.[0]?.Field || [];

            const getLocValue = (key) => {
                const field = locFields.find(f => f.Key === key);
                return field ? field.Value : "";
            };

            console.log(`PrecisionLevel: ${getLocValue("PrecisionLevel")}`);
            console.log(`Type          : ${getLocValue("Type")}`);
            console.log(`Latitude      : ${getLocValue("Latitude")}`);
            console.log(`Longitude     : ${getLocValue("Longitude")}`);

            console.log("\n* Address Components *");
            console.log(`PremiseNumber          : ${getLocValue("PremiseNumber")}`);
            console.log(`Thoroughfare           : ${getLocValue("Thoroughfare")}`);
            console.log(`DoubleDependentLocality: ${getLocValue("DoubleDependentLocality")}`);
            console.log(`DependentLocality      : ${getLocValue("DependentLocality")}`);
            console.log(`Locality               : ${getLocValue("Locality")}`);
            console.log(`AdministrativeArea1    : ${getLocValue("AdministrativeArea1")}`);
            console.log(`AdministrativeArea1Abbr: ${getLocValue("AdministrativeArea1Abbreviation")}`);
            console.log(`AdministrativeArea2    : ${getLocValue("AdministrativeArea2")}`);
            console.log(`AdministrativeArea2Abbr: ${getLocValue("AdministrativeArea2Abbreviation")}`);
            console.log(`AdministrativeArea3    : ${getLocValue("AdministrativeArea3")}`);
            console.log(`AdministrativeArea3Abbr: ${getLocValue("AdministrativeArea3Abbreviation")}`);
            console.log(`AdministrativeArea4    : ${getLocValue("AdministrativeArea4")}`);
            console.log(`AdministrativeArea4Abbr: ${getLocValue("AdministrativeArea4Abbreviation")}`);
            console.log(`PostalCode             : ${getLocValue("PostalCode")}`);
            console.log(`Country                : ${getLocValue("Country")}`);
            console.log(`CountryISO2            : ${getLocValue("CountryISO2")}`);
            console.log(`CountryISO3            : ${getLocValue("CountryISO3")}`);
            console.log(`GoogleMapsURL          : ${getLocValue("GoogleMapsURL")}`);
            console.log(`PlaceName              : ${getLocValue("PlaceName")}`);
            console.log(`IsUnincorporated       : ${getLocValue("IsUnincorporated")}`);
            console.log(`StateFIPS              : ${getLocValue("StateFIPS")}`);
            console.log(`CountyFIPS             : ${getLocValue("CountyFIPS")}`);
            console.log(`CensusTract            : ${getLocValue("CensusTract")}`);
            console.log(`CensusBlock            : ${getLocValue("CensusBlock")}`);
            console.log(`CensusGeoID            : ${getLocValue("CensusGeoID")}`);
            console.log(`ClassFP                : ${getLocValue("ClassFP")}`);
            console.log(`CongressCode           : ${getLocValue("CongressCode")}`);
            console.log(`SLDUST                 : ${getLocValue("SLDUST")}`);
            console.log(`SLDLST                 : ${getLocValue("SLDLST")}`);
            console.log(`Timezone_UTC           : ${getLocValue("Timezone_UTC")}`);
        });
    } catch (err) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${err.message}`);
    }
}

export { ReverseSearchSoapGo };
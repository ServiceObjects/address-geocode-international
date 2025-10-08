![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGI - Address Geocode International

DOTS Address Geocode – International (AGI) is a web service that provides latitude/longitude and metadata information for international addresses and places. AGI is designed to take an international address, geocode it and then return a set of latitudinal and longitudinal coordinates along with any available address component information. 

The AGI service can also be used to search for and geocode non-address places such as neighborhoods, cities and regions by name.

## [Service Objects Website](https://serviceobjects.com)

# AGI - PlaceSearch

The PlaceSearch operation returns the latitude and longitude for a given International Address, along with additional address and location information. It will attempt to geocode addresses at the property level, which is often a rooftop coordinate for some properties and then cascade to the next best available resolution such as the street, neighborhood, postal code, locality and so on. For non-address places such as streets, cities and postal codes, the coordinates will return a coordinate point that is commonly associated with the location or a centroid for the area.

### [PlaceSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-placesearch/)

## Library Usage

```
// 1. Build the input
//
//  Fields:
//        singleLine
//        address1
//        address2
//        address3
//        address4
//        address5
//        locality
//        administrativeArea 
//        postalCode
//        boundaries
//        maxResults
//        searchType
//        extras
//        timeoutSeconds
//        country
//        licenseKey
//        isLive

import { PlaceSearchClient } from '../address-geocode-international/REST/place_search_rest.js';

const singleLine = "";
const address1 = "136 W Canon Perdido St";
const address2 = "Ste D";
const address3 = "";
const address4 = "";
const address5 = "";
const locality = "Santa Barbara";
const administrativeArea = "CA";
const postalCode = "93101";
const country = "USA";
const boundaries = "";
const maxResults = "10";
const searchType = "BestMatch";
const extras = "";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync Invoke() method.
const agi = new PlaceSearchSoap(
    singleLine,
    address1,
    address2,
    address3,
    address4,
    address5,
    locality,
    administrativeArea,
    postalCode,
    country,
    boundaries,
    maxResults,
    searchType,
    extras,
    licenseKey,
    isLive,
    timeoutSeconds
);
const response = await agi.invokeAsync();

// 3. Inspect results.
if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Type    : ${response.Error.Type}`);
    console.log(`Error TypeCode: ${response.Error.TypeCode}`);
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error DescCode: ${response.Error.DescCode}`);
    return;
}

console.log("\n* Search Info *\n");
const fields = response?.Response?.[0]?.Value?.Result?.[0]?.Field || [];
const getValue = (key) =>
{
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

const locationResults = response?.Response?.[1]?.Value?.Result || [];
if (locationResults.length === 0)
{
    console.log("No locations found.");
    return;
}

locationResults.forEach((loc, index) => 
{
    console.log(`\nLocation #${index + 1}`);
    const locFields = loc.Field || [];
    const getLocValue = (key) => 
    {
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
```
# AGI - ReverseSearch

The ReverseSearch operation returns the address or place information for a given set of coordinates, along with additional address and location information. It will attempt to reverse geocode coordinates and return international addresses at the property level, 

which is often at the premise level resolution for some properties and then cascade to the next best available resolution such as the street, neighborhood, postal code, locality and so on. For non-address places such as streets, cities and postal codes returned by the service, the coordinates output in the response body will represent a coordinate point that is commonly associated with the location or a centroid for the area.

### [ReverseSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-reversesearch/)

## Library Usage

```
// 1. Build the input
//
//  fields:
//        latitude
//        longitude
//        searchType
//        searchRadius
//        maxResults
//        country
//        timeoutSeconds
//        isLive
//        licenseKey

import { ReverseSearchSoap } from '../address-geocode-international/SOAP/reverse_search_soap.js';

const latitude = "34.02984234473112";
const longitude = "-118.26876271804397"; 
const searchRadius = "";
const country = "";
const maxResults = "";
const searchType = "BestMatch";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync Invoke() method.
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

// 3. Inspect results.
if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Type    : ${response.Error.Type}`);
    console.log(`Error TypeCode: ${response.Error.TypeCode}`);
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error DescCode: ${response.Error.DescCode}`);
    return;
}

console.log("\n* Search Info *\n");
const fields = response?.Response?.[0]?.Value?.Result?.[0]?.Field || [];
const getValue = (key) => 
{
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

if (locationResponses.length === 0) 
{
    console.log("No locations found.");
    return;
}

locationResponses.forEach((locationResponse, index) => 
{
    console.log(`\nLocation #${index + 1}`);
    const locFields = locationResponse?.Value?.Result?.[0]?.Field || [];

    const getLocValue = (key) => 
    {
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
```

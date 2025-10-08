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
const response = await PlaceSearchClient.invoke(
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
if (response && response.SearchInfo)
{
    console.log(`Status           : ${response.SearchInfo.Status}`);
    console.log(`NumberOfLocations: ${response.SearchInfo.NumberOfLocations}`);
    console.log(`Notes            : ${response.SearchInfo.Notes}`);
    console.log(`NotesDesc        : ${response.SearchInfo.NotesDesc}`);
    console.log(`Warnings         : ${response.SearchInfo.Warnings}`);
    console.log(`WarningDesc      : ${response.SearchInfo.WarningDesc}`);
}
else 
{
    console.log("No search info found.");
}

console.log("\n* Locations *\n");
if (response && response.Locations && response.Locations.length > 0) 
{
    response.Locations.forEach((location, index) => 
    {
        console.log(`\nLocation ${index + 1}:`);
        console.log(`PrecisionLevel: ${location.PrecisionLevel}`);
        console.log(`Type          : ${location.Type}`);
        console.log(`Latitude      : ${location.Latitude}`);
        console.log(`Longitude     : ${location.Longitude}`);

        console.log("\n* Address Components *");
        if (location.AddressComponents) {
            console.log(`PremiseNumber          : ${location.AddressComponents.PremiseNumber}`);
            console.log(`Thoroughfare           : ${location.AddressComponents.Thoroughfare}`);
            console.log(`DoubleDependentLocality: ${location.AddressComponents.DoubleDependentLocality}`);
            console.log(`DependentLocality      : ${location.AddressComponents.DependentLocality}`);
            console.log(`Locality               : ${location.AddressComponents.Locality}`);
            console.log(`AdministrativeArea1    : ${location.AddressComponents.AdministrativeArea1}`);
            console.log(`AdministrativeArea1Abbr: ${location.AddressComponents.AdministrativeArea1Abbreviation}`);
            console.log(`AdministrativeArea2    : ${location.AddressComponents.AdministrativeArea2}`);
            console.log(`AdministrativeArea2Abbr: ${location.AddressComponents.AdministrativeArea2Abbreviation}`);
            console.log(`AdministrativeArea3    : ${location.AddressComponents.AdministrativeArea3}`);
            console.log(`AdministrativeArea3Abbr: ${location.AddressComponents.AdministrativeArea3Abbreviation}`);
            console.log(`AdministrativeArea4    : ${location.AddressComponents.AdministrativeArea4}`);
            console.log(`AdministrativeArea4Abbr: ${location.AddressComponents.AdministrativeArea4Abbreviation}`);
            console.log(`PostalCode             : ${location.AddressComponents.PostalCode}`);
            console.log(`Country                : ${location.AddressComponents.Country}`);
            console.log(`CountryISO2            : ${location.AddressComponents.CountryISO2}`);
            console.log(`CountryISO3            : ${location.AddressComponents.CountryISO3}`);
            console.log(`GoogleMapsURL          : ${location.AddressComponents.GoogleMapsURL}`);
            console.log(`PlaceName              : ${location.AddressComponents.PlaceName}`);
            console.log(`IsUnincorporated       : ${location.AddressComponents.IsUnincorporated}`);
            console.log(`StateFIPS              : ${location.AddressComponents.StateFIPS}`);
            console.log(`CountyFIPS             : ${location.AddressComponents.CountyFIPS}`);
            console.log(`CensusTract            : ${location.AddressComponents.CensusTract}`);
            console.log(`CensusBlock            : ${location.AddressComponents.CensusBlock}`);
            console.log(`CensusGeoID            : ${location.AddressComponents.CensusGeoID}`);
            console.log(`ClassFP                : ${location.AddressComponents.ClassFP}`);
            console.log(`CongressCode           : ${location.AddressComponents.CongressCode}`);
            console.log(`SLDUST                 : ${location.AddressComponents.SLDUST}`);
            console.log(`SLDLST                 : ${location.AddressComponents.SLDLST}`);
            console.log(`Timezone_UTC           : ${location.AddressComponents.Timezone_UTC}`);
        } 
        else
        {
            console.log("No address components found.");
        }
    });
} 
else
{
    console.log("No locations found.");
}
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

import { ReverseSearchClient } from '../address-geocode-international/REST/reverse_search_rest.js';

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
if (response && response.SearchInfo)
{
    console.log(`Status           : ${response.SearchInfo.Status}`);
    console.log(`NumberOfLocations: ${response.SearchInfo.NumberOfLocations}`);
    console.log(`Notes            : ${response.SearchInfo.Notes}`);
    console.log(`NotesDesc        : ${response.SearchInfo.NotesDesc}`);
    console.log(`Warnings         : ${response.SearchInfo.Warnings}`);
    console.log(`WarningDesc      : ${response.SearchInfo.WarningDesc}`);
}
else
{
    console.log("No search info found.");
}

console.log("\n* Locations *\n");
if (response && response.Locations && response.Locations.length > 0)
{
    response.Locations.forEach((location) => 
    {
        console.log(`PrecisionLevel   : ${location.PrecisionLevel}`);
        console.log(`Type             : ${location.Type}`);
        console.log(`Latitude         : ${location.Latitude}`);
        console.log(`Longitude        : ${location.Longitude}`);

        console.log("\n* Address Components *");
        if (location.AddressComponents) 
        {
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
        } 
        else
        {
            console.log("No address components found.");
        }
    });
}
else
{
    console.log("No locations found.");
}
```

import {PlaceSearchRestGo} from './place_search_rest_sdk_example.js'
import {PlaceSearchSoapGo} from './place_search_soap_sdk_example.js'
import {ReverseSearchRestGo} from'./reverse_search_rest_sdk_example.js'
import {ReverseSearchSoapGo} from'./reverse_search_soap_sdk_example.js'
export async function main()
{
    //Your license key from Service Objects.
    //Trial license keys will only work on the
    //trial environments and production license
    //keys will only work on production environments.
    const licenseKey = "LICENSE KEY";
    const isLive = true;
    
    // Address Geocode International - PlaceSearch - REST SDK
    await PlaceSearchRestGo(licenseKey, isLive);

    // Address Geocode International - PlaceSearch - SOAP SDK
    await PlaceSearchSoapGo(licenseKey, isLive);
    
    // Address Geocode International - ReverseSearch - REST SDK
    await ReverseSearchRestGo(licenseKey, isLive);

    // Address Geocode International - ReverseSearch - SOAP SDK
    await ReverseSearchSoapGo(licenseKey, isLive);
}
main().catch((error) => {
  console.error("An error occurred:", error);
  process.exit(1);
});
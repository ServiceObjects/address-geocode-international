![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGI - Address Geocode International

DOTS Address Geocode – International (AGI) is a web service that provides latitude/longitude and metadata information for international addresses and places. AGI is designed to take an international address, geocode it and then return a set of latitudinal and longitudinal coordinates along with any available address component information. The AGI service can also be used to search for and geocode non-address places such as neighborhoods, cities and regions by name.

## [Service Objects Website](https://serviceobjects.com)
## [Developer Guide/Documentation](https://www.serviceobjects.com/docs/)

# AGI - PlaceSearch

The PlaceSearch operation returns the latitude and longitude for a given International Address, along with additional address and location information. It will attempt to geocode addresses at the property level, which is often a rooftop coordinate for some properties and then cascade to the next best available resolution such as the street, neighborhood, postal code, locality and so on. For non-address places such as streets, cities and postal codes, the coordinates will return a coordinate point that is commonly associated with the location or a centroid for the area.

## [PlaceSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-placesearch/)

## PlaceSearch Request URLs (Query String Parameters)

>[!CAUTION]
>### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*


### JSON
#### Trial

https://trial.serviceobjects.com/AGI/api.svc/json/PlaceSearch?SingleLine=&Address1=136+W+Canon+Perdido+St&Address2=Ste+D&Address3=&Address4=&Address5=&Locality=Santa+Barbara&AdministrativeArea=CA&PostalCode=93101&Country=USA&Boundaries=&MaxResults=&SearchType=&Extras=&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AGI/api.svc/json/PlaceSearch?SingleLine=&Address1=136+W+Canon+Perdido+St&Address2=Ste+D&Address3=&Address4=&Address5=&Locality=Santa+Barbara&AdministrativeArea=CA&PostalCode=93101&Country=USA&Boundaries=&MaxResults=&SearchType=&Extras=&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AGI/api.svc/json/PlaceSearch?SingleLine=&Address1=136+W+Canon+Perdido+St&Address2=Ste+D&Address3=&Address4=&Address5=&Locality=Santa+Barbara&AdministrativeArea=CA&PostalCode=93101&Country=USA&Boundaries=&MaxResults=&SearchType=&Extras=&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AGI/api.svc/xml/PlaceSearch?SingleLine=&Address1=136+W+Canon+Perdido+St&Address2=Ste+D&Address3=&Address4=&Address5=&Locality=Santa+Barbara&AdministrativeArea=CA&PostalCode=93101&Country=USA&Boundaries=&MaxResults=&SearchType=&Extras=&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AGI/api.svc/xml/PlaceSearch?SingleLine=&Address1=136+W+Canon+Perdido+St&Address2=Ste+D&Address3=&Address4=&Address5=&Locality=Santa+Barbara&AdministrativeArea=CA&PostalCode=93101&Country=USA&Boundaries=&MaxResults=&SearchType=&Extras=&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AGI/api.svc/xml/PlaceSearch?SingleLine=&Address1=136+W+Canon+Perdido+St&Address2=Ste+D&Address3=&Address4=&Address5=&Locality=Santa+Barbara&AdministrativeArea=CA&PostalCode=93101&Country=USA&Boundaries=&MaxResults=&SearchType=&Extras=&LicenseKey={LicenseKey}

# AGI - ReverseSearch

The ReverseSearch operation returns the address or place information for a given set of coordinates, along with additional address and location information. It will attempt to reverse geocode coordinates and return international addresses at the property level, which is often at the premise level resolution for some properties and then cascade to the next best available resolution such as the street, neighborhood, postal code, locality and so on. For non-address places such as streets, cities and postal codes returned by the service, the coordinates output in the response body will represent a coordinate point that is commonly associated with the location or a centroid for the area.

## [ReverseSearch Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-international/agi-operations/agi-reversesearch/)

## ReverseSearch Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/AGI/api.svc/json/ReverseSearch?Latitude=34.02984234473112&Longitude=-118.26876271804397&SearchRadius=5&Country=USA&MaxResults=3&SearchType=BestMatch&LicenseKey={LicenseKey}

#### Production

https://ws.serviceobjects.com/AGI/api.svc/json/ReverseSearch?Latitude=34.02984234473112&Longitude=-118.26876271804397&SearchRadius=5&Country=USA&MaxResults=3&SearchType=BestMatch&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AGI/api.svc/json/ReverseSearch?Latitude=34.02984234473112&Longitude=-118.26876271804397&SearchRadius=5&Country=USA&MaxResults=3&SearchType=BestMatch&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/AGI/api.svc/xml/ReverseSearch?Latitude=34.02984234473112&Longitude=-118.26876271804397&SearchRadius=5&Country=USA&MaxResults=3&SearchType=BestMatch&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/AGI/api.svc/xml/ReverseSearch?Latitude=34.02984234473112&Longitude=-118.26876271804397&SearchRadius=5&Country=USA&MaxResults=3&SearchType=BestMatch&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/AGI/api.svc/xml/ReverseSearch?Latitude=34.02984234473112&Longitude=-118.26876271804397&SearchRadius=5&Country=USA&MaxResults=3&SearchType=BestMatch&LicenseKey={LicenseKey}

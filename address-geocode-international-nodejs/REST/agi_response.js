export class SearchInfo {
    constructor(data = {}) {
        this.Status = data.Status;
        this.NumberOfLocations = data.NumberOfLocations || 0;
        this.Notes = data.Notes;
        this.NotesDesc = data.NotesDesc;
        this.Warnings = data.Warnings;
        this.WarningDesc = data.WarningDesc;
    }

    toString() {
        return `Status: ${this.Status}, ` +
            `NumberOfLocations: ${this.NumberOfLocations}, ` +
            `Notes: ${this.Notes}, ` +
            `NotesDesc: ${this.NotesDesc}, ` +
            `Warnings: ${this.Warnings}, ` +
            `WarningDesc: ${this.WarningDesc}`;
    }
}

export class AddressComponents {
    constructor(data = {}) {
        this.PremiseNumber = data.PremiseNumber;
        this.Thoroughfare = data.Thoroughfare;
        this.DoubleDependentLocality = data.DoubleDependentLocality;
        this.DependentLocality = data.DependentLocality;
        this.Locality = data.Locality;
        this.AdministrativeArea1 = data.AdministrativeArea1;
        this.AdministrativeArea1Abbreviation = data.AdministrativeArea1Abbreviation;
        this.AdministrativeArea2 = data.AdministrativeArea2;
        this.AdministrativeArea2Abbreviation = data.AdministrativeArea2Abbreviation;
        this.AdministrativeArea3 = data.AdministrativeArea3;
        this.AdministrativeArea3Abbreviation = data.AdministrativeArea3Abbreviation;
        this.AdministrativeArea4 = data.AdministrativeArea4;
        this.AdministrativeArea4Abbreviation = data.AdministrativeArea4Abbreviation;
        this.PostalCode = data.PostalCode;
        this.Country = data.Country;
        this.CountryISO2 = data.CountryISO2;
        this.CountryISO3 = data.CountryISO3;
        this.GoogleMapsURL = data.GoogleMapsURL;
        this.PlaceName = data.PlaceName;
        this.IsUnincorporated = data.IsUnincorporated;
        this.StateFIPS = data.StateFIPS;
        this.CountyFIPS = data.CountyFIPS;
        this.CensusTract = data.CensusTract;
        this.CensusBlock = data.CensusBlock;
        this.CensusGeoID = data.CensusGeoID;
        this.ClassFP = data.ClassFP;
        this.CongressCode = data.CongressCode;
        this.SLDUST = data.SLDUST;
        this.SLDLST = data.SLDLST;
        this.Timezone_UTC = data.Timezone_UTC;
    }

    toString() {
        return `PremiseNumber: ${this.PremiseNumber}, ` +
            `Thoroughfare: ${this.Thoroughfare}, ` +
            `DoubleDependentLocality: ${this.DoubleDependentLocality}, ` +
            `DependentLocality: ${this.DependentLocality}, ` +
            `Locality: ${this.Locality}, ` +
            `AdministrativeArea1: ${this.AdministrativeArea1}, ` +
            `AdministrativeArea1Abbreviation: ${this.AdministrativeArea1Abbreviation}, ` +
            `AdministrativeArea2: ${this.AdministrativeArea2}, ` +
            `AdministrativeArea2Abbreviation: ${this.AdministrativeArea2Abbreviation}, ` +
            `AdministrativeArea3: ${this.AdministrativeArea3}, ` +
            `AdministrativeArea3Abbreviation: ${this.AdministrativeArea3Abbreviation}, ` +
            `AdministrativeArea4: ${this.AdministrativeArea4}, ` +
            `AdministrativeArea4Abbreviation: ${this.AdministrativeArea4Abbreviation}, ` +
            `PostalCode: ${this.PostalCode}, ` +
            `Country: ${this.Country}, ` +
            `CountryISO2: ${this.CountryISO2}, ` +
            `CountryISO3: ${this.CountryISO3}, ` +
            `GoogleMapsURL: ${this.GoogleMapsURL}, ` +
            `PlaceName: ${this.PlaceName}, ` +
            `IsUnincorporated: ${this.IsUnincorporated}, ` +
            `StateFIPS: ${this.StateFIPS}, ` +
            `CountyFIPS: ${this.CountyFIPS}, ` +
            `CensusTract: ${this.CensusTract}, ` +
            `CensusBlock: ${this.CensusBlock}, ` +
            `CensusGeoID: ${this.CensusGeoID}, ` +
            `ClassFP: ${this.ClassFP}, ` +
            `CongressCode: ${this.CongressCode}, ` +
            `SLDUST: ${this.SLDUST}, ` +
            `SLDLST: ${this.SLDLST}, ` +
            `Timezone_UTC: ${this.Timezone_UTC}`;
    }
}
export class LocationInfo {
    constructor(data = {}) {
        this.PrecisionLevel = data.PrecisionLevel || 0;
        this.Type = data.Type;
        this.Latitude = data.Latitude;
        this.Longitude = data.Longitude;
        this.AddressComponents = data.AddressComponents ? new AddressComponents(data.AddressComponents) : null;
    }

    toString() {
        return `PrecisionLevel: ${this.PrecisionLevel}, ` +
            `Type: ${this.Type}, ` +
            `Latitude: ${this.Latitude}, ` +
            `Longitude: ${this.Longitude}, ` +
            `AddressComponents: ${this.AddressComponents ? this.AddressComponents.toString() : 'null'}`;
    }
}
export class Error {
    constructor(data = {}) {
        this.Type = data.Type;
        this.TypeCode = data.TypeCode;
        this.Desc = data.Desc;
        this.DescCode = data.DescCode;
    }

    toString() {
        return `Type: ${this.Type}, TypeCode: ${this.TypeCode}, Desc: ${this.Desc}, DescCode: ${this.DescCode}`;
    }
}

export class PSResponse {
    constructor(data = {}) {
        this.SearchInfo = data.SearchInfo ? new SearchInfo(data.SearchInfo) : null;
        this.Locations = (data.Locations || []).map(location => new LocationInfo(location));
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        const locationsString = this.Locations.length
            ? this.Locations.map(location => location.toString()).join(', ')
            : 'None';
        return `PSResponse: SearchInfo = ${this.SearchInfo ? this.SearchInfo.toString() : 'null'}, ` +
            `Locations = [${locationsString}], ` +
            `Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}
export class RSResponse {
    constructor(data = {}) {
        this.SearchInfo = data.SearchInfo ? new SearchInfo(data.SearchInfo) : null;
        this.Locations = (data.Locations || []).map(location => new LocationInfo(location));
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        let result = 'RSResponse:\n';
        if (this.SearchInfo) {
            result += `SearchInfo: ${this.SearchInfo.toString()}\n`;
        }
        if (this.Locations && this.Locations.length > 0) {
            result += 'Locations:\n';
            result += this.Locations.map(location => location.toString()).join('\n') + '\n';
        }
        if (this.Error) {
            result += `Error: ${this.Error.toString()}`;
        }
        return result;
    }
}

export default { PSResponse, RSResponse };
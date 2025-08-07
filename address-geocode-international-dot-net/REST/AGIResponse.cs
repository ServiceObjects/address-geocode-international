using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace address_geocode_international_dot_net.REST
{
    /// <summary>
    /// Represents the AGI ReverseSearch API response (REST).
    /// </summary>
    [DataContract]
    public class AGIReverseSearchResponse
    {
        public AGIReverseSearchResponse()
        {
            Locations = new List<Location>();
        }

        [DataMember(Name = "SearchInfo")]
        public SearchInfo? SearchInfo { get; set; }

        [DataMember(Name = "Locations")]
        public List<Location> Locations { get; set; }

        [DataMember(Name = "Error")]
        public ErrorDetails? Error { get; set; }

        public override string ToString()
        {
            string output = "AGI Reverse Search Response:\n";
            output += SearchInfo != null ? SearchInfo.ToString() : "  SearchInfo: null\n";

            output += Locations != null && Locations.Count > 0
                ? "  Locations:\n" +
                    string.Join("", Locations.Select((loc, i) => $"    [{i + 1}]:\n{loc}"))
                : "  Locations: []\n";

            output += Error != null ? Error.ToString() : "  Error: null\n";
            return output;
        }
    }

    /// <summary>
    /// Represents the AGI PlaceSearch API response (REST).
    /// </summary>
    [DataContract]
    public class AGIPlaceSearchResponse
    {
        [DataMember(Name = "SearchInfo")]
        public SearchInfo? SearchInfo { get; set; }

        [DataMember(Name = "Locations")]
        public Location[]? Locations { get; set; }

        [DataMember(Name = "Error")]
        public ErrorDetails? Error { get; set; }

        public override string ToString()
        {
            string output = "AGI Place Search Response:\n";
            output += SearchInfo != null ? SearchInfo.ToString() : "  SearchInfo: null\n";

            if (Locations != null && Locations.Length > 0)
            {
                output += "  Locations:\n";
                for (int i = 0; i < Locations.Length; i++)
                {
                    output += $"    [{i + 1}]:\n{Locations[i]}\n";
                }
            }
            else
            {
                output += "  Locations: []\n";
            }

            output += Error != null ? Error.ToString() : "  Error: null\n";
            return output;
        }
    }

    /// <summary>
    /// Search information block returned by the AGI service.
    /// </summary>
    [DataContract]
    public class SearchInfo
    {
        [DataMember(Name = "Status")]
        public string? Status { get; set; }

        [DataMember(Name = "NumberOfLocations")]
        public int? NumberOfLocations { get; set; }

        [DataMember(Name = "Notes")]
        public string? Notes { get; set; }

        [DataMember(Name = "NotesDesc")]
        public string? NotesDesc { get; set; }

        [DataMember(Name = "Warnings")]
        public string? Warnings { get; set; }

        [DataMember(Name = "WarningDesc")]
        public string? WarningDesc { get; set; }

        public override string ToString()
        {
            string output = "  SearchInfo:\n";
            output += $"    Status           : {Status}\n";
            output += $"    NumberOfLocations: {NumberOfLocations}\n";
            output += $"    Notes            : {Notes}\n";
            output += $"    NotesDesc        : {NotesDesc}\n";
            output += $"    Warnings         : {Warnings}\n";
            output += $"    WarningDesc      : {WarningDesc}\n";
            return output;
        }
    }

    /// <summary>
    /// Represents a geocoded location as returned by the AGI service.
    /// </summary>
    [DataContract]
    public class Location
    {
        public Location()
        {
            AddressComponents = new AddressComponents();
        }

        [DataMember(Name = "PrecisionLevel")]
        public double PrecisionLevel { get; set; }

        [DataMember(Name = "Type")]
        public string? Type { get; set; }

        [DataMember(Name = "Latitude")]
        public string? Latitude { get; set; }

        [DataMember(Name = "Longitude")]
        public string? Longitude { get; set; }

        [DataMember(Name = "AddressComponents")]
        public AddressComponents? AddressComponents { get; set; }

        [DataMember(Name = "PlaceName")]
        public string? PlaceName { get; set; }

        [DataMember(Name = "GoogleMapsURL")]
        public string? GoogleMapsURL { get; set; }

        [DataMember(Name = "BingMapsURL")]
        public string? BingMapsURL { get; set; }

        [DataMember(Name = "MapQuestURL")]
        public string? MapQuestURL { get; set; }

        [DataMember(Name = "StateFIPS")]
        public string? StateFIPS { get; set; }

        [DataMember(Name = "CountyFIPS")]
        public string? CountyFIPS { get; set; }

        [DataMember(Name = "ClassFP")]
        public string? ClassFP { get; set; }

        public override string ToString()
        {
            string output = "";
            output += $"      Type          : {Type}\n";
            output += $"      PrecisionLevel: {PrecisionLevel}\n";
            output += $"      Latitude      : {Latitude}\n";
            output += $"      Longitude     : {Longitude}\n";
            output += $"      PlaceName     : {PlaceName}\n";
            output += $"      GoogleMapsURL : {GoogleMapsURL}\n";
            output += $"      BingMapsURL   : {BingMapsURL}\n";
            output += $"      MapQuestURL   : {MapQuestURL}\n";
            output += $"      StateFIPS     : {StateFIPS}\n";
            output += $"      CountyFIPS    : {CountyFIPS}\n";
            output += $"      ClassFP       : {ClassFP}\n";
            output += AddressComponents != null
                ? AddressComponents.ToString()
                : "      AddressComponents: null\n";
            return output;
        }
    }

    /// <summary>
    /// Address components returned within a location result.
    /// </summary>
    [DataContract]
    public class AddressComponents
    {
        [DataMember(Name = "PremiseNumber")]
        public string? PremiseNumber { get; set; }

        [DataMember(Name = "Thoroughfare")]
        public string? Thoroughfare { get; set; }

        [DataMember(Name = "DoubleDependentLocality")]
        public string? DoubleDependentLocality { get; set; }

        [DataMember(Name = "DependentLocality")]
        public string? DependentLocality { get; set; }

        [DataMember(Name = "Locality")]
        public string? Locality { get; set; }

        [DataMember(Name = "AdministrativeArea1")]
        public string? AdministrativeArea1 { get; set; }

        [DataMember(Name = "AdministrativeArea1Abbreviation")]
        public string? AdministrativeArea1Abbreviation { get; set; }

        [DataMember(Name = "AdministrativeArea2")]
        public string? AdministrativeArea2 { get; set; }

        [DataMember(Name = "AdministrativeArea2Abbreviation")]
        public string? AdministrativeArea2Abbreviation { get; set; }

        [DataMember(Name = "AdministrativeArea3")]
        public string? AdministrativeArea3 { get; set; }

        [DataMember(Name = "AdministrativeArea3Abbreviation")]
        public string? AdministrativeArea3Abbreviation { get; set; }

        [DataMember(Name = "AdministrativeArea4")]
        public string? AdministrativeArea4 { get; set; }

        [DataMember(Name = "AdministrativeArea4Abbreviation")]
        public string? AdministrativeArea4Abbreviation { get; set; }

        [DataMember(Name = "PostalCode")]
        public string? PostalCode { get; set; }

        [DataMember(Name = "Country")]
        public string? Country { get; set; }

        [DataMember(Name = "CountryISO2")]
        public string? CountryISO2 { get; set; }

        [DataMember(Name = "CountryISO3")]
        public string? CountryISO3 { get; set; }

        [DataMember(Name = "GoogleMapsURL")]
        public string? GoogleMapsURL { get; set; }

        [DataMember(Name = "PlaceName")]
        public string? PlaceName { get; set; }

        [DataMember(Name = "IsUnincorporated")]
        public string? IsUnincorporated { get; set; }

        [DataMember(Name = "TimeZone_UTC")]
        public string? TimeZone_UTC { get; set; }

        [DataMember(Name = "CongressCode")]
        public string? CongressCode { get; set; }

        [DataMember(Name = "CensusTract ")]
        public string? CensusTract { get; set; }

        [DataMember(Name = "CensusGeoID ")]
        public string? CensusGeoID { get; set; }

        [DataMember(Name = "ClassFP")]
        public string? ClassFP { get; set; }

        [DataMember(Name = "StateFIPS ")]
        public string? StateFIPS { get; set; }

        [DataMember(Name = "SLDUST ")]
        public string? SLDUST { get; set; }

        [DataMember(Name = "SLDLST ")]
        public string? SLDLST { get; set; }

        [DataMember(Name = "CountyFIPS ")]
        public string? CountyFIPS { get; set; }

        [DataMember(Name = "CensusBlock ")]
        public string? CensusBlock { get; set; }
        public override string ToString()
        {
            string output = "      AddressComponents:\n";
            output += $"        PremiseNumber                  : {PremiseNumber}\n";
            output += $"        Thoroughfare                   : {Thoroughfare}\n";
            output += $"        DoubleDependentLocality        : {DoubleDependentLocality}\n";
            output += $"        DependentLocality              : {DependentLocality}\n";
            output += $"        Locality                       : {Locality}\n";
            output += $"        AdministrativeArea1            : {AdministrativeArea1}\n";
            output += $"        AdministrativeArea1Abbreviation: {AdministrativeArea1Abbreviation}\n";
            output += $"        AdministrativeArea2            : {AdministrativeArea2}\n";
            output += $"        AdministrativeArea2Abbreviation:{AdministrativeArea2Abbreviation}\n";
            output += $"        AdministrativeArea3            :{AdministrativeArea3}\n";
            output += $"        AdministrativeArea3Abbreviation:{AdministrativeArea3Abbreviation}\n";
            output += $"        AdministrativeArea4            :{AdministrativeArea4}\n";
            output += $"        AdministrativeArea4Abbreviation:{AdministrativeArea4Abbreviation}\n";
            output += $"        PostalCode                     : {PostalCode}\n";
            output += $"        Country                        : {Country}\n";
            output += $"        CountryISO2                    : {CountryISO2}\n";
            output += $"        CountryISO3                    : {CountryISO3}\n";
            output += $"        TimeZone_UTC                   : {TimeZone_UTC}\n";
            output += $"        CongressCode                   : {CongressCode}\n";
            output += $"        GoogleMapsURL                  : {GoogleMapsURL}\n";
            output += $"        PlaceName                      : {PlaceName}\n";
            output += $"        IsUnincorporated               : {IsUnincorporated}\n";
            output += $"        StateFIPS                      : {StateFIPS}\n";
            output += $"        CountyFIPS                     : {CountyFIPS}\n";
            output += $"        CensusTract                    : {CensusTract}\n";
            output += $"        CensusBlock                    : {CensusBlock}\n";
            output += $"        CensusGeoID                    : {CensusGeoID}\n";
            output += $"        ClassFP                        : {ClassFP}\n";
            output += $"        SLDUST                         : {SLDUST}\n";
            output += $"        SLDLST                         : {SLDLST}\n";


            return output;
        }
    }

    /// <summary>
    /// Error block returned if the API fails or receives invalid input.
    /// </summary>
    [DataContract]
    public class ErrorDetails
    {
        [DataMember(Name = "Type")]
        public string? Type { get; set; }

        [DataMember(Name = "TypeCode")]
        public string? TypeCode { get; set; }

        [DataMember(Name = "Desc")]
        public string? Desc { get; set; }

        [DataMember(Name = "DescCode")]
        public string? DescCode { get; set; }

        public override string ToString()
        {
            string output = "  Error:\n";
            output += $"    Type    : {Type}\n";
            output += $"    TypeCode: {TypeCode}\n";
            output += $"    Desc    : {Desc}\n";
            output += $"    DescCode: {DescCode}\n";
            return output;
        }
    }
}

using System.Collections.Generic;

namespace ApiTesting.Objects
{
    public class CountryData
    {
        public string Name { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public List<int?> CallingCodes { get; set; }
        public string Capital { get; set; }
        public List<string> AltSpellings { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public double? Population { get; set; }
        public List<double?> Latlng { get; set; }
        public string Demonym { get; set; }
        public double? Area { get; set; }
        public double? Gini { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Borders { get; set; }
        public string NativeName { get; set; }
        public int? NumericCode { get; set; }
        public List<LegalTender> Currencies { get; set; }
        public List<MethodOfCommunication> Languages { get; set; }
        public LanguageTranslation Translations { get; set; }
        public string Flag { get; set; }
        public List<GeographicalTerritorial> RegionalBlocs { get; set; }
        public string Cioc { get; set; }
    }
}

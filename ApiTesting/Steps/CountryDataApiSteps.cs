using System;
using System.Collections.Generic;
using System.Linq;
using ApiTesting.Base;
using ApiTesting.Objects;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ApiTesting.Steps
{
    [Binding]
    public class CountryDataApiSteps
    {
        private readonly CountryDataLayer _countryDataLayer = new CountryDataLayer();
        private Action _sendingCapitalCityName;
        private Action _sendingRegionName;
        private Action _checkCountriesAndTheirExpectedBorderingCountries;
        private IList<CountryData> _actualResults;
        private int _borderCount;

        [Given(@"I have hit the required endpoint by capitalCity")]
        public void GivenIHaveHitTheRequiredEndpointByCapitalCity(Table table)
        {
            dynamic tableDetails = table.CreateDynamicInstance();
            _sendingCapitalCityName = () => _actualResults = _countryDataLayer.GetCountryDataByCapitalName<CountryData>(tableDetails.capitalCity).Result;
        }

        [Then(@"I should get a result back")]
        public void ThenIShouldGetAResultBack()
        {
            _sendingCapitalCityName.Should().NotThrow();
        }

        [Given(@"I have hit the required endpoint by region")]
        public void GivenIHaveHitTheRequiredEndpointByRegion(Table table)
        {
            dynamic tableDetails = table.CreateDynamicInstance();
            string requestedRegion = tableDetails.region;

            _sendingRegionName = () => _actualResults = _countryDataLayer.GetCountryDataByRegion<CountryData>(requestedRegion).Result;
            _sendingRegionName.Should().NotThrow();
        }

        [Given(@"I should be able to filter by bordering countries")]
        public void GivenIShouldBeAbleToFilterByBorderingCountries(Table table)
        {
            dynamic tableDetails = table.CreateDynamicInstance();
            var countriesWithMorethan3Borders = _actualResults.Where(c => c.Borders.Count() > tableDetails.filterByNumberOfBorderingCountries);

            foreach (var countryNames in countriesWithMorethan3Borders)
            {
                var name = countryNames.Name;
                Console.WriteLine("{0} have bordering countries more than {1} : ", name, tableDetails.filterByNumberOfBorderingCountries);
            }
        }

        [Given(@"I have hit the required endpoint by region and filtered by country")]
        public void GivenIHaveHitTheRequiredEndpointByRegionAndFilteredByCountry(Table table)
        {
            dynamic tableDetails = table.CreateDynamicInstance();
            string requestedRegion = tableDetails.region;

            _checkCountriesAndTheirExpectedBorderingCountries = () => _actualResults = _countryDataLayer.GetCountryDataByRegion<CountryData>(requestedRegion).Result;
            _checkCountriesAndTheirExpectedBorderingCountries.Should().NotThrow();

            var countriesAndthereBorders = _actualResults.Where(item => item.Name.Contains(tableDetails.filterByCountries));

            foreach (var countryNames in countriesAndthereBorders)
            {
                _borderCount = countryNames.Borders.Count;
            }

            //var departingCountry = _actualResults.Where(item => item.Name == tableDetails.filterByCountries).ToList()
            //                                             .Select(item => item.Borders).ToList();

            //var arrivingCountry = _actualResults.Where(item => item.Name == "Germany").ToList()
            //                                              .Select(item => item.Borders).ToList();

            //var randomIndex = new Random().Next(0, departingCountry.Count - 1);
            //var randomFirstName = departingCountry[randomIndex];
            //return randomFirstName;

            //.ForEach(item =>
            //   {
            //       var results = item.Where(x => 
            //       x.Name.Contains(tableDetails.filterByCountries))
            //    });
        }

        [Then(@"I the actual bordering country should match as expected")]
        public void ThenITheActualBorderingCountryShouldMatchAsExpected(Table table)
        {
            dynamic tableDetails = table.CreateDynamicInstance();
            Assert.AreEqual(tableDetails.expectedBorderingCountry, _borderCount);
        }
    }
}

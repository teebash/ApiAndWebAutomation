Feature: CountryDataApi
	In validate Api response for country details


@mytag
Scenario: Check api respons by capital city 
	Given I have hit the required endpoint by capitalCity    
    | capitalCity |
    | london      |
	Then I should get a result back


@mytag
Scenario: Find countries with bordering countries by region 
	Given I have hit the required endpoint by region    
    | region |
    | europe |
    And I should be able to filter by bordering countries
    | filterByNumberOfBorderingCountries |
    | 5                                  |


@mytag
Scenario: Find countries in a specific region with there respective bordering countries 
	Given I have hit the required endpoint by region and filtered by country
    | region | filterByCountries |
    | europe | Germany           |
	Then I the actual bordering country should match as expected
    | expectedBorderingCountry |
    | 9                        |
Feature: LoginEA
	In order for customers to be able to access EA app using valid credentials

	@Chrome
Scenario: Login into EA web application
	Given I have navigated to the web page
	And I have logged in with valid credentials
	When I click login button
	Then the users home page should be displayed

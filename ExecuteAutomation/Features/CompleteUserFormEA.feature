Feature: CompleteUserFormEA
	Successfully logged in customers are able to complete user form and save it successfully

	@Chrome @LoggedIn
Scenario: Fill out user form 
	Given I am on the user form page
	And I fill out the user form
	When I click save
	Then the user details is saved successfully

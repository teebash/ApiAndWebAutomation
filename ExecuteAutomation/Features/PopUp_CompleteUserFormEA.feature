Feature: PopUp_CompleteUserFormEA
	Verify logged in customers can also complete user form using the pop up feature

		@Chrome @LoggedIn
Scenario: Fill out user form on the Pop up window
	Given I am on the user form page
	And I have clicked the htmlPopUp to display the user form pop up
	When I fill out the user form
	Then the user details is saved successfully

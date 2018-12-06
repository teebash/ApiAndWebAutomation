using ExecuteAutomation.Customer;
using ExecuteAutomation.Pages;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Steps
{
    [Binding]
    public class CompleteUserFormEaSteps
    {
        private readonly UserFormEaPage _userFormEaPage;
        private readonly ScenarioContext _scenarioContext;

        public CompleteUserFormEaSteps(UserFormEaPage userFormEaPage, ScenarioContext scenarioContext)
        {
            _userFormEaPage = userFormEaPage;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the user form page")]
        public void GivenIAmOnTheUserFormPage()
        {
            var customer = _scenarioContext.Get<ICustomer>();

            _userFormEaPage.VerifyUsersHomePageIsDisplayed();
        }

        [Given(@"I have clicked the htmlPopUp to display the user form pop up")]
        public void GivenIHaveClickedTheHtmlPopUpToDisplayTheUserFormPopUp()
        {
            _userFormEaPage.ClickUserFormPopUp();
        }

        [Given(@"I fill out the user form")]
        [When(@"I fill out the user form")]
        public void GivenIFillOutTheUserForm()
        {
            var customer = _scenarioContext.Get<ICustomer>();

            _userFormEaPage.CompleteUserForm(customer.Title, customer.Initial, customer.FirstName,
                customer.MiddleName, customer.LastName, customer.Gender, "Hindi", customer.Country);
        }

        [When(@"I click save")]
        public void WhenIClickSave()
        {
            _userFormEaPage.SaveCompletedForm();
        }

        [Then(@"the user details is saved successfully")]
        public void ThenTheUserDetailsIsSavedSuccessfully()
        {
            _userFormEaPage.VerifyUsersHomePageIsDisplayed();
            _userFormEaPage.LogoutFromEa();
        }
    }
}

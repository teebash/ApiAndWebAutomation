using ExecuteAutomation.Customer;
using ExecuteAutomation.Pages;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Steps
{
    [Binding]
    public class LoginEaSteps
    {
        private readonly LoginEaPage _loginEa;
        private readonly UserFormEaPage _userFormEaPage;
        private readonly ICustomer _customer = new CustomerFactory().CreateCustomer(new GetGender().GetRandomGender()); 

        public LoginEaSteps(LoginEaPage loginEa, UserFormEaPage userFormEaPage)
        {
            _loginEa = loginEa;
            _userFormEaPage = userFormEaPage;
        }

        [Given(@"I have navigated to the web page")]
        public void GivenIHaveNavigatedToTheWebPage()
        {
            _loginEa.Open();
        }
        
        [Given(@"I have logged in with valid credentials")]
        public void GivenIHaveLoggedInWithValidCredentials()
        {
            _loginEa.FillUserLoginDetails(_customer.UserName, _customer.Password);
        }

        [When(@"I click login button")]
        public void WhenIClickLoginButton()
        {
            _loginEa.Login();
        }

        [Then(@"the users home page should be displayed")]
        public void ThenTheUsersHomePageShouldBeDisplayed()
        {
            _userFormEaPage.VerifyUsersHomePageIsDisplayed();
        }
    }
}

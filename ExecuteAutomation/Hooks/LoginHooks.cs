using ExecuteAutomation.Customer;
using ExecuteAutomation.Pages;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Hooks
{
    [Binding]
    public class LoginHooks
    {
        private readonly LoginEaPage _loginEaPage;
        private readonly ICustomer _customer = new CustomerFactory().CreateCustomer(new GetGender().GetRandomGender());
        private readonly ScenarioContext _scenarioContext;

        public LoginHooks(LoginEaPage loginEaPage, ScenarioContext scenarioContext)
        {
            _loginEaPage = loginEaPage;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("@LoggedIn", Order = HookOrdering.LoggedIn)]
        public void LoginUserBeforScenario()
        {
            _scenarioContext.Set(_customer);

            _loginEaPage.Open();
            _loginEaPage.FillUserLoginDetails(_customer.UserName, _customer.Password);
            _loginEaPage.Login();
        }
    }
}

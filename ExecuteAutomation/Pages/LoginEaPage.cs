using ExecuteAutomation.Base;
using ExecuteAutomation.WebDriverExtensions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Pages
{
    [Binding]
    public class LoginEaPage : PageBase
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly WebDriverHelpers _driverHelpers;
        public const string Url = "/Login.html";

        public LoginEaPage(IWebDriver driver, ScenarioContext scenarioContext, WebDriverHelpers driverHelpers) : base(driver, scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _driverHelpers = driverHelpers;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "input[name*='UserName']")]
        private IWebElement UserName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='Password']")]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='Login']")]
        private IWebElement LoginBtn { get; set; }

        internal void Open()
        {
            Open(Url);
        }

        public void FillUserLoginDetails(string userName, string password)
        {
            _driverHelpers.PerformKeyboardTypingOperation(UserName, userName);
            _driverHelpers.PerformKeyboardTypingOperation(Password, password);
        }

        public UserFormEaPage Login()
        {
            _driverHelpers.PerformClickOperation(LoginBtn);

            return new UserFormEaPage(_driver, _scenarioContext, _driverHelpers);
        }
    }
}

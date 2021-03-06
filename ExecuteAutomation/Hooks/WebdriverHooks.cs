﻿using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Hooks
{
    [Binding]
    public class WebdriverHooks
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WebdriverHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeScenario("@Chrome", Order = HookOrdering.UseChromeDriver)]
        public void WebDriverSettingsForChrome()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [BeforeScenario("@FireFox", Order = HookOrdering.UseFireFoxDriver)]
        public void WebDriverSettingsForFireFox()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                Log.Error(_scenarioContext.TestError.Message);
                Log.Error(_scenarioContext.TestError.StackTrace);
            }

            Log.Info(_scenarioContext.ScenarioExecutionStatus.ToString());
            Log.Info(" Finished Test Scenario : " + _scenarioContext.ScenarioInfo.Title);

            Log.Info("==================================================================================");
            Log.Info("=====  "    + _featureContext.FeatureInfo.Title + " Test Ended               =====");
            Log.Info("==================================================================================");

            _driver?.Close();
            _driver?.Dispose();
        }
    }
}

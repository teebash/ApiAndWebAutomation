using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExecuteAutomation.Base;
using ExecuteAutomation.WebDriverExtensions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Pages
{
    [Binding]
    public class UserFormEaPage : PageBase
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly WebDriverHelpers _driverHelpers;
        public ReadOnlyCollection<string> OpenWindows;

        public UserFormEaPage(IWebDriver driver, ScenarioContext scenarioContext, WebDriverHelpers driverHelpers) : base(driver, scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _driverHelpers = driverHelpers;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "TitleId")]
        private IWebElement Title { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='Initial']")]
        private IWebElement Initial { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='FirstName']")]
        private IWebElement FirstName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='MiddleName']")]
        private IWebElement MiddleName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='LastName']")]
        private IWebElement LastName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tr:nth-child(5)")]
        private IList<IWebElement> Gender { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tr:nth-child(6)")]
        private IList<IWebElement> LanguagesKnown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[href*='popup.html']")]
        private IWebElement HtmlPopElement { get; set; }

        [FindsBy(How = How.Name, Using = "Country")]
        private IWebElement CountryDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='Save']")]
        private IWebElement SaveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#cssmenu > ul > li:nth-child(1) > a > span")]
        private IWebElement LogoutBtnElement { get; set; }


        public void CompleteUserForm(string title, string initial, string firstName, string middleName, string lastName, string gender,
            string language, string country)
        {
            // sets the window count
            OpenWindows = _driver.WindowHandles;

            if (OpenWindows.Count < 2)
            {
                _driverHelpers.PerformSelectFromDropDownByText(Title, title);
                _driverHelpers.PerformKeyboardTypingOperation(Initial, initial);
                _driverHelpers.PerformKeyboardTypingOperation(FirstName, firstName);
                _driverHelpers.PerformKeyboardTypingOperation(MiddleName, middleName);
                Gender.Select(x => x.FindElement(By.CssSelector("input[name*='" + gender + "']"))).ToList().ForEach(x => x.Click());
                LanguagesKnown.Select(x => x.FindElement(By.CssSelector("input[name*='" + language + "']"))).ToList().ForEach(x => x.Click());
            }
            else
            {
                // switches to popup window
                _driver.SwitchTo().Window(OpenWindows[1]);

                _driverHelpers.PerformSelectFromDropDownByText(Title, title);
                _driverHelpers.PerformKeyboardTypingOperation(Initial, initial);
                _driverHelpers.PerformKeyboardTypingOperation(FirstName, firstName);
                _driverHelpers.PerformKeyboardTypingOperation(MiddleName, middleName);
                _driverHelpers.PerformKeyboardTypingOperation(LastName, lastName);
                Gender.Select(x => x.FindElement(By.CssSelector("input[name*='" + gender + "']"))).ToList().ForEach(x => x.Click());
                _driverHelpers.PerformSelectFromDropDownByText(CountryDropDown, country);
                _driverHelpers.PerformClickOperation(SaveBtn);

                // Closses the pop up windown
                _driver.Close();
                // resets the windown count variable
                OpenWindows = _driver.WindowHandles;

                // switches back to parent window
                if (OpenWindows.Count == 1)
                {
                    _driver.SwitchTo().Window(OpenWindows[0]);
                }
                else
                {
                    throw new Exception("Parent window not found");
                }
            }

        }

        public void SaveCompletedForm()
        {
            SaveBtn.Click();
        }

        public void VerifyUsersHomePageIsDisplayed()
        {
            bool displayed = LogoutBtnElement.Displayed;
            if (displayed.Equals(true))
            {
                var contains = _driver.PageSource.Contains("User Form");
            }
            else
            {
                throw new Exception("User Form Page is not displayed");
            }
        }

        public void ClickUserFormPopUp()
        {
            _driverHelpers.PerformClickOperation(HtmlPopElement);
        }

        public void LogoutFromEa()
        {
            _driverHelpers.PerformClickOperation(LogoutBtnElement);
        }
    }
}

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ExecuteAutomation.WebDriverExtensions
{
    [Binding]
    public class WebDriverHelpers
    {
        private readonly IWebDriver _driver;

        public WebDriverHelpers(IWebDriver driver)
        {
            _driver = driver;
        }

        internal bool WaitUtillDisplayed(IWebDriver driver, IWebElement element, int time = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromMilliseconds(time)).Until(_driver => element.Displayed);
        }

        internal void WaitUtilClickable(IWebDriver driver, IWebElement element, int time = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromMilliseconds(time)).Until(
                ExpectedConditions.ElementToBeClickable(element));
        }

        public void PerformClickOperation(IWebElement element)
        {
            WaitUtilClickable(_driver, element);
            element.Click();
        }

        public void PerformKeyboardTypingOperation(IWebElement element, string textValue)
        {
            WaitUtilClickable(_driver, element);
            element.SendKeys(textValue);
        }

        public void PerformSelectFromDropDownByText(IWebElement element, string text)
        {
            WaitUtillDisplayed(_driver, element);
            new SelectElement(element).SelectByText(text);
        }
    }
}

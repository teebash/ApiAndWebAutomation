using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Base
{
    /// <summary>
    ///     This is the page base from which all other pages extend.
    /// </summary>
    [Binding]
    public abstract class PageBase
    {
        protected readonly IWebDriver Driver;

        /// <summary>
        ///     Construct the page base using the driver, template and scenario context.
        /// </summary>
        /// <param name="driver">The current driver.</param>
        /// <param name="scenarioContext">The current scenario context.</param>
        protected PageBase(IWebDriver driver, ScenarioContext scenarioContext)
        {
            Driver = driver;
            ScenarioContext = scenarioContext;
            UrlParams = new Dictionary<string, object>();
        }

        // Used locator used to determine when the page is fully loaded.
        //protected By IsRenderedWhenElementDisplayed => By.Id("main-wrapper");

        protected ScenarioContext ScenarioContext { get; }

        protected Dictionary<string, object> UrlParams { get; }

        /// <summary>
        ///     Open a page!
        /// </summary>
        /// <param name="uri">The URI to open.</param>
        public void Open(string uri)
        {
            // Create a 'Regex' object from the URI passed in from the page concrete.
            var regexUri = new Regex(uri);
            var matcher = regexUri.Match(uri);

            // Swap out the named capture groups for the actual values.
            try
            {
                uri = UrlParams.Aggregate(uri,
                    (current, param) => current.Replace(matcher.Groups[param.Key].Value, param.Value.ToString())
                );
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(
                    "Url Replace Failed: Mismatch between named capture groups and replacement values.");
            }

            // If the URL (with the swapped out capture groups) doesn't match the current URL then load it.
            if (!CurrentUrlMatches(uri)) Driver.Navigate().GoToUrl(GetUrl(uri));

            PageFactory.InitElements(Driver, this);
        }

        /// <summary>
        /// Does the current URL match a pages URI.
        /// </summary>
        /// <param name="uri">The URI to match with.</param>
        /// <returns>true if it does, false otherwise.</returns>
        public bool CurrentUrlMatches(string uri)
        {
            return Regex.IsMatch(Driver.Url, GetUrl(uri));
        }

        /// <summary>
        ///     Get a fully qualified URL as defined by the AppSettings base url.
        /// </summary>
        /// <param name="uri">The URI to attach to the base URL.</param>
        /// <returns>The fully qualified page.</returns>
        public string GetUrl(string uri)
        {
            return ConfigurationManager.AppSettings.Get("baseurl") + uri;
        }
    }
}

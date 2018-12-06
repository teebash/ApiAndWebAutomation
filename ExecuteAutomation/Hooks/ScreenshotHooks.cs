﻿using System;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace ExecuteAutomation.Hooks
{
    [Binding]
    public class ScreenshotHooks
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string ScreenshotDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");

        public ScreenshotHooks(IWebDriver driver, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [AfterStep()]
        public void TakeScreenshotAfterStep()
        {
            var afterScreenshotFileName = $"{DateTime.Now:mm-ss-fff}.png";
            var fullScreenshotPath = Path.Combine(ScreenshotDirectory, $"{_featureContext.FeatureInfo.Title}", $"{_scenarioContext.ScenarioInfo.Title}");

            if (!Directory.Exists(fullScreenshotPath))
            {
                Directory.CreateDirectory(fullScreenshotPath);
            }

            if (_scenarioContext.TestError == null) return;
            {
                var takesScreenshot = _driver as ITakesScreenshot;
                var afterStepScreenshot = takesScreenshot?.GetScreenshot();
                afterStepScreenshot?.SaveAsFile(Path.Combine(fullScreenshotPath, afterScreenshotFileName), ScreenshotImageFormat.Png);
            }
        }

        [BeforeTestRun]
        public static void BeforTestRun()
        {
            if (!Directory.Exists(ScreenshotDirectory)) return;
            {
                if (Directory.GetDirectories(ScreenshotDirectory).Length <= 0) return;
                {
                    foreach (var directory in Directory.GetDirectories(ScreenshotDirectory))
                    {
                        Directory.Delete(Path.Combine(ScreenshotDirectory, directory), true);
                    }
                }
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (Directory.Exists(ScreenshotDirectory))
            {
                var screenshotDirectoryInfo = new DirectoryInfo(ScreenshotDirectory);
                foreach (var screenshot in screenshotDirectoryInfo.GetFiles())
                {
                    if (screenshot.CreationTime >= DateTime.Now.AddDays(1)) continue;
                    {
                        screenshot.Delete();
                    }
                }
            }
        }
    }
}

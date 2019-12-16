namespace SeleniumTestsSDK.Utils
{
    using System;
    using System.Linq;
    using Containers;
    using Helpers;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public static class DriverExtensions
    {
        public static void GoTo(this IWebDriver driver, string url)
        {
            try
            {
                driver.Url = url;
            }
            catch (WebDriverTimeoutException)
            {
                // catch and ignore
            }
        }

        public static void SwitchToWindow(this IWebDriver driver, string windowHandle)
        {
            driver.WaitForSeconds(1);
            driver.SwitchTo().Window(windowHandle);
            WebPageConsumer.InFrame = false;
        }

        public static void SwitchToLastWindow(this IWebDriver driver)
        {
            driver.WaitForSeconds(1);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebPageConsumer.InFrame = false;
        }

        public static void SwitchToTab(this IWebDriver driver, int tabIndex)
        {
            driver.WaitForSeconds(1);

            if (driver.WindowHandles.Count >= tabIndex)
            {
                driver.SwitchTo().Window(driver.WindowHandles[tabIndex - 1]);
                WebPageConsumer.InFrame = false;
            }
        }

        public static void WaitForSeconds(this IWebDriver driver, int seconds)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromSeconds(seconds) > TimeSpan.Zero);
        }

        public static bool IsDisplayed(this IWebElement element, int seconds = 3)
        {
            return element != null && Methods.WaitUntil(() => element.Displayed, seconds);
        }

        public static IWebElement SafeClick(this IWebElement element, IWebDriver driver = null)
        {
            if (driver == null)
            {
                driver = WebDriverProvider.DriverInstance;
            }

            if (IsDisplayed(element))
            {
                WebPageConsumer.CurrentElement = element;

                try
                {
                    element.Click();

                    return element;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is WebDriverTimeoutException)
                    {
                        // ignore
                    }
                }

                return element;
            }

            throw new ArgumentException($"Cannot click since element was not found");
        }
    }
}

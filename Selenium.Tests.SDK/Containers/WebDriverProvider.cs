namespace Selenium.Tests.SDK.Containers
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using Settings;
    using Settings.Selenium;
    using Utils;

    public class WebDriverProvider
    {
        private static readonly SeleniumSettings settings;

        static WebDriverProvider()
        {
            settings = SeleniumSettings.Current;
        }

        public static IWebDriver DriverInstance;

        public static IWebDriver GetWebDriver()
        {
            if (settings.Mode.Equals("remote", StringComparison.OrdinalIgnoreCase))
            {
                var driverKind = SettingsManager.GetBrowser().ToLower();
                Logger.WriteInfo($"{driverKind} browser was found in appSettings.json");
                return GetRemoteWebDriver((BrowserKind)Enum.Parse(typeof(BrowserKind), driverKind, true));
            }

            return GetLocalWebDriver();
        }

        private static IWebDriver GetRemoteWebDriver(BrowserKind browser)
        {
            var endpoint = settings.Endpoint;
            var capabilities = new DesiredCapabilities(settings.Capabilities(browser));
            Logger.WriteInfo($"{capabilities.BrowserName} browser will be used in tests");
            return new RemoteWebDriver(endpoint, capabilities);
        }

        private static IWebDriver GetLocalWebDriver()
        {
            if (settings.Browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                Logger.WriteInfo($"Local chrome driver will be used");
                return new ChromeDriver();
            }

            throw new Exception("Selenium settings has unknown browser");
        }
    }
}
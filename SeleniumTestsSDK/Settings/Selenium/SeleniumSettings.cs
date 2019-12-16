namespace SeleniumTestsSDK.Settings.Selenium
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    public class SeleniumSettings
    {
        private static SeleniumSettings current;

        private SeleniumSettings()
        {
        }

        public static SeleniumSettings Current => current ?? (current = new SeleniumSettings());

        public string Mode => Configuration["BrowserSessionSettings:Mode"];

        public Uri Endpoint => new Uri(Configuration["RemoteBrowserSettings:Endpoint"]);

        public string Browser => Configuration["LocalBrowserSettings:Browser"];

        public Dictionary<string, object> Capabilities(BrowserKind browserKind)
        {
            string browserCapabilities = string.Empty;

            switch (browserKind)
            {
                case BrowserKind.Auto:
                    browserCapabilities = "ChromeCapabilities";
                    break;
                case BrowserKind.Chrome:
                    browserCapabilities = "ChromeCapabilities";
                    break;
                case BrowserKind.Firefox:
                    browserCapabilities = "FirefoxCapabilities";
                    break;
                case BrowserKind.operablink:
                    browserCapabilities = "OperaCapabilities";
                    break;
                case BrowserKind.MicrosoftEdge:
                    browserCapabilities = "EdgeCapabilities";
                    break;
            }

            var path = ResolveFilePath("seleniumSettings.json");
            using (var file = File.OpenText(path))
            {
                var text = file.ReadToEnd();
                var parsedSettings = JObject.Parse(text);
                var capabilities = parsedSettings[Configuration[$"RemoteBrowserSettings:{browserCapabilities}"]];

                return JsonDeserializer.GetDictionaryFromJObject((JObject)capabilities);
            }
        }

        private static IConfigurationRoot Configuration { get; } = SettingsBuilder();

        private static IConfigurationRoot SettingsBuilder()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("seleniumSettings.json");
            return builder.Build();
        }

        private static string ResolveFilePath(string path)
        {
            return Path.Combine(AppContext.BaseDirectory, path);
        }
    }
}
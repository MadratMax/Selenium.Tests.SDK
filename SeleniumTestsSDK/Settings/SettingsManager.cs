using System;
using Microsoft.Extensions.Configuration;

namespace SeleniumTestsSDK.Settings
{
    internal class SettingsManager
    {
        private static string currentBrowser;

        public static IConfigurationRoot Configuration { get; } = SettingsBuilder();

        public static IUser GetUser(string role)
        {
            currentBrowser = GetBrowser();
            
            IUser user = new User
            {
                UserName = Configuration[$"tests:users:{currentBrowser}:{role}:username"],
                Password = Configuration[$"tests:users:{currentBrowser}:{role}:password"],
                Role = role,
            };

            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentNullException($"User '{role}' does not exist. Check appSettings.json");
            }

            return user;
        }

        public static string GetBrowser()
        {
            return GetSettingsValue("tests", "browser").ToLower();
        }

        public static string GetPackage(string version)
        {
            return Configuration[$"tests:sfdxPackage:url"];
        }

        public static bool SkipPackage()
        {
            var skipValue = GetSettingsValue("tests", "skipPackage");
            return skipValue.ToLower() == "true";
        }

        public static string GetDevice(string user)
        {
            return GetSettingsValue("tests", "users", currentBrowser, user, "deviceName");
        }

        public static string GetScreenShotPath()
        {
            return Configuration[$"tests:screenshot:path"];
        }

        public static string GetSettingsValue(params string[] args)
        {
            if (args.Length == 1)
            {
                return Configuration[$"{args[0]}"];
            }

            int index = 0;
            string paramsString = string.Empty;

            foreach (var arg in args)
            {
                paramsString = $"{paramsString}{args[index]}:";
                index++;
            }

            return Configuration[paramsString.TrimEnd(':')];
        }

        private static IConfigurationRoot SettingsBuilder()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}

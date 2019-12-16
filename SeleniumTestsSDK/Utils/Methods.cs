namespace SeleniumTestsSDK.Utils
{
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Polly;

    public static class Methods
    {
        public static bool WaitUntil(Func<bool> action, int maxWait = 15)
        {
            Policy
                .HandleResult<bool>(r => r == false)
                .WaitAndRetry(maxWait, retryAttempt => TimeSpan.FromSeconds(1))
                .Execute(action);

            return true;
        }

        public static void WaitForSeconds(int maxWait = 5)
        {
            Thread.Sleep(maxWait * 1000);
        }

        public static string RemoveRestrictedChars(string input)
        {
            char[] symbols = { ':', '*', '|', '/', '\\', '?', '"', '<', '>', '\'', '\r', '\n' };
            foreach (char symbol in symbols)
            {
                input = input.Replace(symbol, ' ');
            }

            int index = input.IndexOf(",", StringComparison.Ordinal);

            if (index > 0)
            {
                input = input.Substring(0, index);
            }

            return input;
        }

        public static string TruncateAndNormalize(this string value, int maxChars)
        {
            var updatedString = value.Length <= maxChars ? value : value.Substring(0, maxChars).Trim();

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            updatedString = regex.Replace(updatedString, " ");

            return updatedString;
        }

        public static void KillChromeDriver()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                try
                {
                    process.Kill();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}

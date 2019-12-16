namespace SeleniumTestsSDK.Utils
{
    using System.IO;
    using OpenQA.Selenium;
    using Settings;

    public class ScreenCapture
    {
        private readonly string screenShotPath;
        private readonly IWebDriver driver;

        public ScreenCapture(IWebDriver driver, string screenShotPath)
        {
            this.driver = driver;
            this.screenShotPath = screenShotPath;
        }

        public void TakeScreenshot(string scenario, string stepName, string testError)
        {
            var browserName = SettingsManager.GetSettingsValue("tests", "browser").ToLower();

            if (!Directory.Exists(this.screenShotPath))
            {
                Directory.CreateDirectory(this.screenShotPath);
            }

            string failedScenarioPath = Path.Combine(this.screenShotPath, scenario);

            if (!Directory.Exists(failedScenarioPath))
            {
                Directory.CreateDirectory(failedScenarioPath);
            }

            var allWindowHandles = this.driver.WindowHandles;

            foreach (var win in allWindowHandles)
            {
                var index = allWindowHandles.IndexOf(win);
                this.driver.SwitchTo().Window(win);
                Screenshot ss = ((ITakesScreenshot)this.driver).GetScreenshot();
                var correctedStepName = Methods.RemoveRestrictedChars(stepName);
                var correctedError = Methods.RemoveRestrictedChars(testError);
                var screenShotFullName = $"{failedScenarioPath}\\{correctedStepName}_{correctedError}[{index + 1}].png";
                ss.SaveAsFile(screenShotFullName, ScreenshotImageFormat.Png);
            }
        }
    }
}

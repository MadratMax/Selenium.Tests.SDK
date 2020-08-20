namespace SeleniumTestsSDK.Bindings
{
    using System;
    using OpenQA.Selenium;
    using Settings;
    using TechTalk.SpecFlow;
    using Utils;

    [Binding]
    public class BaseSteps : Steps
    {
        private readonly IWebDriver driver;

        public BaseSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string WindowHandle { get; set; }

        private string CurrentStepName { get; set; }

        [BeforeScenario(Order = 1)]
        public void Setup()
        {
            //var options = new ChromeOptions();
            //options.AddArgument("--allow-file-access-from-files");
            //options.AddArgument("--enable-file-cookies");
            //options.AddArguments("--start-maximized");
        }

        [BeforeStep]
        public void BeforeStepAction()
        {
            this.CurrentStepName = ScenarioContext.Current.StepContext.StepInfo.Text;
        }

        [AfterScenario(Order = 1)]
        private void TearDown()
        {
            var error = ScenarioContext.Current.TestError;

            if (error != null)
            {
                string screenShotPath = SettingsManager.GetSettingsValue("tests", "screenshot", "path");
                string testName = ScenarioContext.Current.ScenarioInfo.Title.TruncateAndNormalize(70);
                string stepName = this.CurrentStepName.TruncateAndNormalize(50);
                string testError = Methods.RemoveRestrictedChars(ScenarioContext.Current.TestError.Message).TruncateAndNormalize(60);
                new ScreenCapture(this.driver, screenShotPath).TakeScreenshot(testName, stepName, testError);
                Logger.Write($"screen has been captured. image stored in {screenShotPath}\\{testName} with name: '{stepName}_{testError}'");
            }

            try
            {
                this.driver?.Quit();
                this.driver?.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Methods.KillChromeDriver();
            }
        }
    }
}

namespace SeleniumTestsSDK.Bindings.Hooks
{
    using Engines;
    using OpenQA.Selenium;
    using Pages;
    using TechTalk.SpecFlow;
    using Utils;

    [Binding]
    internal class BrowserHookSteps : BaseSteps
    {
        private readonly IWebDriver driver;
        private readonly IPages pages;
        private readonly ISearchEngine engine;

        public BrowserHookSteps(
            IWebDriver driver,
            IPages pages,
            ISearchEngine engine)
            : base(driver)
        {
            this.driver = driver;
            this.pages = pages;
            this.engine = engine;
        }

        [When(@"user switches to pop up window")]
        public void SwitchToPopUp()
        {
            this.driver.SwitchToLastWindow();
        }

        [When(@"user switches to last window")]
        public void SwitchToLastWindow()
        {
            this.driver.SwitchToLastWindow();
        }

        [When(@"user switches to (.*) browser tab")]
        public void SwitchToTab(int tabIndex)
        {
            this.driver.SwitchToTab(tabIndex);
        }

        [When(@"user closes (.*) browser tab")]
        public void CloseTab(int tabIndex)
        {
            this.driver.SwitchToTab(tabIndex);
            this.driver.Close();
            this.driver.SwitchToLastWindow();
        }

        [When(@"user refreshes page")]
        public void RefreshPage()
        {
            this.RefreshPageInSeconds();
        }

        [When(@"user refreshes page in (.*) seconds")]
        public void RefreshPageInSeconds(int seconds = 0)
        {
            Methods.WaitForSeconds(seconds);
            this.driver.Navigate().Refresh();
        }

        [When(@"user waits (.*) seconds")]
        public void WaitSeconds(int seconds)
        {
            Methods.WaitForSeconds(seconds);
        }

        [When(@"user switches to frame")]
        public void SwitchToFrame()
        {
            this.driver.SwitchToFrame();
        }
    }
}
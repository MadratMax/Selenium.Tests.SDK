namespace SeleniumTestsSDK.Pages
{
    using System.Collections.Generic;
    using Engines;
    using Helpers;
    using OpenQA.Selenium;

    internal class GoogleHomePage : BasePage
    {
        private readonly ISearchEngine engine;
        private readonly IWebDriver driver;

        public GoogleHomePage(
            IWebDriver driver,
            ISearchEngine engine)
        {
            this.driver = driver;
            this.engine = engine;
        }

        public override string Url => URL.GoogleMainPage;

        public bool Displayed => this.IsDisplayed(this, this.SearchBox);

        public IWebElement SearchBox => this.engine.Find(By.XPath("//input[@name='q']"));

        public IWebElement SubmitButton => this.engine.Find(By.XPath("//input[@type='submit']"));
    }
}
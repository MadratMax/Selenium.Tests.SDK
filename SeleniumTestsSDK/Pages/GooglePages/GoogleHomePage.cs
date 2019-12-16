using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumTestsSDK.Engines;
using SeleniumTestsSDK.Helpers;

namespace SeleniumTestsSDK.Pages
{
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
    }
}
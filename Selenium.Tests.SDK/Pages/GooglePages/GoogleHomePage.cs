namespace Selenium.Tests.SDK.Pages
{
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

        public IWebElement SearchButton => this.engine.Find(By.XPath("//input[@type='submit']"));

        public IWebElement NginxLink => this.engine.Find(By.XPath("//a[@href='https://www.nginx.com/']"));
    }
}
namespace Selenium.Tests.SDK.Pages
{
    using OpenQA.Selenium;

    internal interface IBasePage
    {
        string Url { get; set; }

        bool IsDisplayed(BasePage page, IWebElement element);
    }
}
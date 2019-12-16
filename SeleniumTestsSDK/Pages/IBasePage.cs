using OpenQA.Selenium;

namespace SeleniumTestsSDK.Pages
{
    internal interface IBasePage
    {
        string Url { get; set; }

        bool IsDisplayed(BasePage page, IWebElement element);
    }
}
namespace Selenium.Tests.SDK.Pages
{
    using Helpers;
    using OpenQA.Selenium;

    public abstract class BasePage : IBasePage
    {
        public virtual string Url { get; set; }

        public virtual bool IsDisplayed(BasePage page, IWebElement element)
        {
            WebPageConsumer.CurrentPage = page;

            return element != null && element.Displayed;
        }
    }
}

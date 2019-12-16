namespace SeleniumTestsSDK.Engines
{
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using Pages;

    public interface ISearchEngine
    {
        IWebElement Find(By by);

        IWebElement QuickFind(By by, int seconds);

        IEnumerable<IWebElement> QuickMultipleFind(By by);

        IWebElement FindElementByName(BasePage page, string elementName);

        string FindPageUrl(BasePage page);

        bool PageIsDisplayed(BasePage page);
    }
}

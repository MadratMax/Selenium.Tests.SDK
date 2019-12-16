namespace SeleniumTestsSDK.Bindings.Hooks
{
    using System.Linq;
    using Engines;
    using Helpers;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using Pages;
    using TechTalk.SpecFlow;
    using Utils;

    [Binding]
    internal class WebHookSteps : BaseSteps
    {
        private readonly IWebDriver driver;
        private readonly IPages pages;
        private readonly ISearchEngine engine;

        public WebHookSteps(
            IWebDriver driver,
            IPages pages,
            ISearchEngine engine)
            : base(driver)
        {
            this.driver = driver;
            this.pages = pages;
            this.engine = engine;
        }

        [Given(@"user clicks on '(.*)'")]
        [When(@"user clicks on '(.*)'")]
        public void WhenUserClicksOn(string elementName)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            Assert.IsNotNull(webElement, $"{elementName} was not found on page {currentPage.GetPageName()}");

            WebPageConsumer.CurrentElement = webElement;
            Methods.WaitUntil(() => webElement.IsDisplayed());
            webElement.SafeClick(this.driver);
        }

        [When(@"user clicks on '(.*)' in (.*) seconds")]
        public void WhenUserClickInSeconds(string elementName, int seconds)
        {
            Methods.WaitForSeconds(seconds);

            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            Assert.IsNotNull(webElement, $"{elementName} was not found on page {currentPage.GetPageName()}");

            WebPageConsumer.CurrentElement = webElement;
            Methods.WaitUntil(() => webElement.IsDisplayed());
            webElement.SafeClick();
        }

        [Given(@"user opens page '(.*)'")]
        [When(@"user opens page '(.*)'")]
        public void UserOpensPage(string pageName)
        {
            var desiredPage = this.pages.GetPages().FirstOrDefault(p => p.GetType().Name == pageName);

            Assert.IsNotNull(desiredPage, $"{pageName} was not found in page objects set");

            var pageUrl = this.engine.FindPageUrl(desiredPage);

            Assert.IsNotNull(pageUrl, $"Cannot find URL from the page: {pageName}. Check the 'Url' property for {pageName}");

            this.driver.GoTo(pageUrl);

            var pageIsDisplayed = this.engine.PageIsDisplayed(desiredPage);

            Assert.IsTrue(pageIsDisplayed, $"{desiredPage.GetPageName()} was not displayed");
        }

        [When(@"page '(.*)' is displayed")]
        [Then(@"page '(.*)' is displayed")]
        public void PageIsDisplayed(string pageName)
        {
            var desiredPage = this.pages.GetPages().FirstOrDefault(p => p.GetType().Name == pageName);

            Assert.IsNotNull(desiredPage, $"{pageName} was not found in page objects set");

            var pageIsDisplayed = this.engine.PageIsDisplayed(desiredPage);

            Assert.IsTrue(pageIsDisplayed, $"{pageName} was not displayed");
        }

        // this step should be used only after the step: user clicks on '(.*)'
        [When(@"user enters the text '(.*)'")]
        public void UserEntersText(string text)
        {
            var webElement = WebPageConsumer.CurrentElement;

            if (webElement == null)
            {
                Assert.Fail("Web element was not found in WebPageConsumer. Check that step 'user clicks on (.*)' was executed before.");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            webElement.SendKeys($"{text}");

            Methods.WaitForSeconds(1);
        }

        [When(@"element '(.*)' is displayed")]
        [Then(@"element '(.*)' is displayed")]
        public void ElementIsDisplayed(string elementName)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            if (webElement == null)
            {
                Assert.Fail($"{elementName} was not found on page {currentPage.GetPageName()}");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            Assert.IsTrue(webElement.IsDisplayed(), $"{elementName} is not displayed");
        }

        [When(@"element '(.*)' is enabled")]
        [Then(@"element '(.*)' is enabled")]
        public void ElementIsEnabled(string elementName)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            if (webElement == null)
            {
                Assert.Fail($"{elementName} was not found on page {currentPage.GetPageName()}");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            Assert.IsTrue(webElement.IsDisplayed(), $"{elementName} is not displayed");

            Methods.WaitUntil(() => webElement.Enabled, 2);
            Assert.IsTrue(webElement.Enabled, $"{elementName} should be enabled, but was not");
        }

        [When(@"element '(.*)' is disabled")]
        [Then(@"element '(.*)' is disabled")]
        public void ElementIsDisabled(string elementName)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            if (webElement == null)
            {
                Assert.Fail($"{elementName} was not found on page {currentPage.GetPageName()}");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            Assert.IsTrue(webElement.IsDisplayed(), $"{elementName} is not displayed");

            Methods.WaitUntil(() => webElement.Enabled == false, 2);
            Assert.IsTrue(webElement.Enabled == false, $"{elementName} should be disabled, but was not");
        }

        [When(@"element '(.*)' text is '(.*)'")]
        [Then(@"element '(.*)' text is '(.*)'")]
        public void ElementTextIs(string elementName, string expectedText)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            if (webElement == null)
            {
                Assert.Fail($"{elementName} was not found on page {currentPage.GetPageName()}");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            var actualElementText = webElement.GetAttribute("value");

            if (string.IsNullOrEmpty(actualElementText))
            {
                actualElementText = webElement.Text;
            }

            Assert.AreEqual(
                expectedText,
                actualElementText);
        }

        [When(@"element '(.*)' contains text '(.*)'")]
        [Then(@"element '(.*)' contains text '(.*)'")]
        public void ElementTextContains(string elementName, string expectedText)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            if (webElement == null)
            {
                Assert.Fail($"{elementName} was not found on page {currentPage.GetPageName()}");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            var actualElementText = webElement.GetAttribute("value");

            if (string.IsNullOrEmpty(actualElementText))
            {
                actualElementText = webElement.Text;
            }

            Assert.IsTrue(
                actualElementText.Contains(expectedText),
                $"Expected: comment text should contain text '{expectedText}'\n Actual text: '{actualElementText}'");
        }

        [When(@"element '(.*)' text is not '(.*)'")]
        [Then(@"element '(.*)' text is not '(.*)'")]
        public void ElementTextIsNot(string elementName, string expectedText)
        {
            var currentPage = WebPageConsumer.CurrentPage;
            var webElement = this.engine.FindElementByName(currentPage, elementName);

            if (webElement == null)
            {
                Assert.Fail($"{elementName} was not found on page {currentPage.GetPageName()}");
            }

            Methods.WaitUntil(() => webElement.IsDisplayed());

            var actualElementText = webElement.GetAttribute("value");

            if (string.IsNullOrEmpty(actualElementText))
            {
                actualElementText = webElement.Text;
            }

            Assert.AreNotEqual(
                expectedText,
                actualElementText);
        }
    }
}

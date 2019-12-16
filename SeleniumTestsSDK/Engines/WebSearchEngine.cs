using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using SeleniumTestsSDK.Helpers;
using SeleniumTestsSDK.Pages;
using SeleniumTestsSDK.Utils;

namespace SeleniumTestsSDK.Engines
{
    internal class WebSearchEngine : ISearchEngine
    {
        private readonly IWebDriver driver;

        public WebSearchEngine(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement Find(By by)
        {
            ISearchContext context = this.driver;
            IWebElement element = null;

            Methods.WaitUntil(() => (element = this.FindElement(context, by)) != null);

            if (element == null)
            {
                if (by.ToString().Contains("//button"))
                {
                    Logger.Write($"Button wasn't found ({by})");

                    var similarElements = this.GetElementsOnPageByTag("button");

                    if (similarElements.Any())
                    {
                        var buttonList = new List<string>();

                        foreach (var el in similarElements)
                        {
                            buttonList.Add(el.Text);
                        }

                        var otherButtons = string.Join(", ", buttonList.ToArray());
                        Logger.WriteInfo($"{buttonList.Count()} other buttons were found on the page {WebPageConsumer.CurrentPageName}: \n{otherButtons}");
                    }
                }

                Logger.Write($"Element wasn't found ({by})");

                return null;
            }

            context = element;

            return context as IWebElement;
        }

        public IWebElement QuickFind(By by, int seconds)
        {
            ISearchContext context = this.driver;
            IWebElement element = null;

            Methods.WaitUntil(() => (element = this.FindElement(context, by)) != null, seconds);

            if (element == null)
            {
                return null;
            }

            context = element;

            return context as IWebElement;
        }

        public IEnumerable<IWebElement> QuickMultipleFind(By by)
        {
            ISearchContext context = this.driver;
            IEnumerable<IWebElement> elements = null;

            elements = this.FindElements(context, by);

            if (elements == null)
            {
                return null;
            }

            return elements;
        }

        public IWebElement FindElementByName(BasePage page, string elementName)
        {
            Type t = page.GetType();
            PropertyInfo prop = t.GetProperty(elementName);
            IWebElement element = (IWebElement)prop?.GetValue(page);
            return element;
        }

        public string FindPageUrl(BasePage page)
        {
            Type t = page.GetType();
            PropertyInfo prop = t.GetProperty("Url");
            string pageUrl = (string)prop?.GetValue(page);
            return pageUrl;
        }

        public bool PageIsDisplayed(BasePage page)
        {
            Type t = page.GetType();
            PropertyInfo prop = t.GetProperty("Displayed");
            var displayed = prop?.GetValue(page);
            return displayed?.ToString().ToLower() == "true";
        }

        private bool IsAvailable(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        private IWebElement FindElement(ISearchContext context, By locator)
        {
            try
            {
                var element = context.FindElement(locator);
                if (!this.IsAvailable(element))
                {
                    return null;
                }

                return element;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
            catch (NotFoundException)
            {
                return null;
            }
            catch (TimeoutException)
            {
                Methods.WaitForSeconds(2);
                var element = context.FindElement(locator);
                if (this.IsAvailable(element))
                {
                    return element;
                }

                return null;
            }
        }

        private IEnumerable<IWebElement> FindElements(ISearchContext context, By locator)
        {
            var visibleElements = new List<IWebElement>();

            try
            {
                var elements = context.FindElements(locator);

                foreach (var element in elements)
                {
                    if (!this.IsAvailable(element))
                    {
                        visibleElements.Add(element);
                    }
                }

                return visibleElements;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        private IEnumerable<IWebElement> GetElementsOnPageByTag(string tagName)
        {
            IEnumerable<IWebElement> elementsOnPage = this.driver.FindElements(
               By.TagName(tagName));

            return elementsOnPage;
        }
    }
}

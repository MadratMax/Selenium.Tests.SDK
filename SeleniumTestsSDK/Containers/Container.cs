using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BoDi;
using OpenQA.Selenium;
using SeleniumTestsSDK.Engines;
using SeleniumTestsSDK.Helpers;
using SeleniumTestsSDK.Pages;
using TechTalk.SpecFlow;

namespace SeleniumTestsSDK.Containers
{
    [Binding]
    internal class Container
    {
        private readonly IObjectContainer objectContainer;
        private IWebDriver driver;
        private ISearchEngine engine;
        private List<BasePage> pageInstances;

        private Container(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 0)]
        private void TypesRegistration()
        {
            this.driver = WebDriverProvider.GetWebDriver();
            this.objectContainer.RegisterInstanceAs<IWebDriver>(this.driver);
            WebDriverProvider.DriverInstance = this.driver;

            this.engine = new WebSearchEngine(this.driver);
            this.objectContainer.RegisterInstanceAs<ISearchEngine>(this.engine);
        }

        [BeforeScenario(Order = 0)]
        private void PageObjectsRegistration()
        {
            var assembly = Assembly.GetExecutingAssembly();
            this.pageInstances = new List<BasePage>();

            foreach (var type in this.FindDerivedTypes(assembly, typeof(BasePage)))
            {
                var instance = this.GetInstance<BasePage>(type, this.driver, this.engine);
                this.pageInstances.Add(instance);
            }

            var pages = new Pages.Pages(this.pageInstances);
            this.objectContainer.RegisterInstanceAs<IPages>(pages);

            WebPageConsumer.Pages = pages;
        }

        private T GetInstance<T>(Type type, params object[] args)
        {
            return (T)Activator.CreateInstance(type, args);
        }

        private IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(b => baseType.IsAssignableFrom(b)).Where(t => t.Name != typeof(BasePage).Name);
        }
    }
}

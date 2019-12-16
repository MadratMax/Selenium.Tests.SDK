using OpenQA.Selenium;
using SeleniumTestsSDK.Pages;
using SeleniumTestsSDK.Utils;

namespace SeleniumTestsSDK.Helpers
{
    internal static class WebPageConsumer
    {
        private static BasePage currentPage;

        public static BasePage CurrentPage
        {
            get => GetCurrentPage();
            set => currentPage = value;
        }

        public static string CurrentPageName => CurrentPage.GetPageName();

        public static IPages Pages { get; set; }

        public static IWebElement CurrentElement { get; set; }

        public static bool InFrame { get; set; }

        public static string GetPageName(this BasePage page)
        {
            return page.GetType().Name;
        }

        private static BasePage GetCurrentPage()
        {
            Logger.WriteInfo($"Current page: {currentPage.GetPageName()}");

            return currentPage;
        }
    }
}
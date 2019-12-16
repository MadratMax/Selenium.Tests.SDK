using System.Collections.Generic;
using System.Linq;

namespace SeleniumTestsSDK.Pages
{
    public class Pages : IPages
    {
        private readonly List<BasePage> pages;

        public Pages(List<BasePage> pages)
        {
            this.pages = pages;
        }

        public List<BasePage> GetPages()
        {
            return this.pages;
        }

        public T Get<T>()
            where T : BasePage
        {
            var page = this.GetPages().FirstOrDefault(n => n is T);

            var instance = (T)page;

            return instance;
        }
    }
}

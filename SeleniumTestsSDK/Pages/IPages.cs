using System.Collections.Generic;

namespace SeleniumTestsSDK.Pages
{
    public interface IPages
    {
        List<BasePage> GetPages();

        T Get<T>()
            where T : BasePage;
    }
}

namespace Selenium.Tests.SDK.Pages
{
    using System.Collections.Generic;

    public interface IPages
    {
        List<BasePage> GetPages();

        T Get<T>()
            where T : BasePage;
    }
}

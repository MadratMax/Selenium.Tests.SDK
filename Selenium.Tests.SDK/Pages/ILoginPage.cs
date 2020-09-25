namespace Selenium.Tests.SDK.Pages
{
    using OpenQA.Selenium;

    public interface ILoginPage
    {
        IWebElement UserNameTextField();

        IWebElement PasswordTextField();

        IWebElement LoginBtn();
    }
}

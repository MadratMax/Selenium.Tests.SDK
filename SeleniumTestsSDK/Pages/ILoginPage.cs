namespace SeleniumTestsSDK
{
    using OpenQA.Selenium;

    public interface ILoginPage
    {
        IWebElement UserNameTextField();

        IWebElement PasswordTextField();

        IWebElement LoginBtn();
    }
}

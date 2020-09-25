namespace Selenium.Tests.SDK.Pages
{
    using Engines;
    using Helpers;
    using OpenQA.Selenium;
    using Settings;

    internal class LoginPage : BasePage, ILoginPage
    {
        private readonly ISearchEngine engine;
        private readonly IWebDriver driver;

        public LoginPage(
            IWebDriver driver,
            ISearchEngine engine)
        {
            this.driver = driver;
            this.engine = engine;
        }

        public bool Displayed => this.IsDisplayed(this, this.UserNameTxtField);

        private IWebElement UserNameTxtField => this.driver.FindElement(By.Id("username"));

        private IWebElement PasswordTxtField => this.driver.FindElement(By.Id("password"));

        private IWebElement LoginBtn => this.driver.FindElement(By.Id("Login"));

        public void LoginWithCreds(IUser user)
        {
            Authorization.Login(this.driver, this, user);
        }

        public IWebElement UserNameTextField()
        {
            return this.UserNameTxtField;
        }

        public IWebElement PasswordTextField()
        {
            return this.PasswordTxtField;
        }

        IWebElement ILoginPage.LoginBtn()
        {
            return this.LoginBtn;
        }
    }
}

namespace SeleniumTestsSDK.Helpers
{
    using System;
    using OpenQA.Selenium;
    using Settings;
    using Utils;

    public class Authorization
    {
        public static void Login(IWebDriver driver, ILoginPage loginPage, IUser user)
        {
            var userName = loginPage.UserNameTextField().GetAttribute("value");
            var pass = loginPage.PasswordTextField().GetAttribute("value");

            if (string.IsNullOrEmpty(userName))
            {
                loginPage.UserNameTextField().SendKeys(user.UserName);
                loginPage.PasswordTextField().SendKeys(user.Password);
                loginPage.LoginBtn().SafeClick();
            }
            else if (userName == user.UserName && string.IsNullOrEmpty(pass))
            {
                Logger.Write($"{loginPage.GetType().Name} text field already contains username {userName}");
                loginPage.PasswordTextField().SendKeys(user.Password);
                loginPage.LoginBtn().SafeClick();
            }
            else
            {
                loginPage.UserNameTextField().Clear();
                loginPage.PasswordTextField().Clear();
                loginPage.UserNameTextField().SendKeys(user.UserName);
                loginPage.PasswordTextField().SendKeys(user.Password);
                loginPage.LoginBtn().SafeClick();
            }

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }
    }
}

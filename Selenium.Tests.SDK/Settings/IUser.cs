namespace Selenium.Tests.SDK.Settings
{
    public interface IUser
    {
        string Role { get; }

        string UserName { get; }

        string Password { get; }
    }
}

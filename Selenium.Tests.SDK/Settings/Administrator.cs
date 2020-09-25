namespace Selenium.Tests.SDK.Settings
{
    internal class Administrator : IUser
    {
        private readonly string userName;

        private readonly string password;

        private Administrator()
        {
            var adminUser = SettingsManager.GetUser(this.Role);

            this.userName = adminUser.UserName;
            this.password = adminUser.Password;
        }

        public string Role => "Administrator";

        public string Password => this.password;

        public string UserName => this.userName;

        public static IUser User()
        {
            return new Administrator();
        }
    }
}

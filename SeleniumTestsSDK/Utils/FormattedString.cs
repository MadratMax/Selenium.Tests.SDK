using System.Text.RegularExpressions;
using SeleniumTestsSDK.Settings;

namespace SeleniumTestsSDK.Utils
{
    public sealed class FormattedString
    {
        private const string Opentag = "<";
        private const string Closetag = ">";

        private readonly string input;
        private readonly string output;

        public FormattedString(string input)
        {
            this.input = input;
            this.output = this.ReplacePlaceholders(this.input);
        }

        public string Input => this.input;

        public int Length => this.output.Length;

        public static implicit operator string(FormattedString x) => x.ToString();

        public static implicit operator FormattedString(string x) => new FormattedString(x);

        public override string ToString() => this.output;

        private string ReplacePlaceholders(string input)
        {
            if (input.Contains(Opentag) && input.Contains(Closetag))
            {
                return input;
            }

            if (!input.Contains(Opentag) && !input.Contains(Closetag))
            {
                var placeHolders = Regex.Matches(input, "<[A-Za-z0-9_]*>");

                for (int i = 0; i < placeHolders.Count;)
                {
                    var placeHolder = placeHolders[i].Value;
                    var key = placeHolder.Substring(1, placeHolder.Length - 2);
                    return SettingsManager.GetPackage(key);
                }
            }

            return SettingsManager.GetPackage(input);
        }
    }
}

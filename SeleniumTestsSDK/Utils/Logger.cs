using System;

namespace SeleniumTestsSDK.Utils
{
    internal class Logger
    {
        public static void Write(string message)
        {
            Console.Out.WriteLine($"-> {message}");
        }

        public static void WriteInfo(string infoMessage)
        {
            Console.Out.WriteLine($"[info] {infoMessage}");
        }

        public static void Write(Exception exception)
        {
            Console.Out.WriteLine(exception.ToString());
        }
    }
}

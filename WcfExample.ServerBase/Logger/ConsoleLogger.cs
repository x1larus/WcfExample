using System;

namespace WcfExample.ServerBase.Logger
{
    internal class ConsoleLogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"[info]: {message}");
        }
    }
}

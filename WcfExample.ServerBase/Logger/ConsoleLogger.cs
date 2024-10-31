using System;
using WcfExample.Shared.Extensions;

namespace WcfExample.ServerBase.Logger
{
    public class ConsoleLogger : IServerLogger
    {
        public void Log(string message, LogType type = LogType.Info)
        {
            var ts = DateTime.Now.ToString("hh:mm:ss.fff");
            var color = ConsoleColor.White; ;

            switch (type)
            {
                case LogType.Info:
                    color = ConsoleColor.White;
                    break;
                case LogType.Warn:
                    color = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    color = ConsoleColor.Red;
                    break;
                case LogType.ServiceUsage:
                    color = ConsoleColor.Green;
                    break;
            }

            Console.ForegroundColor = color;
            Console.WriteLine($"[{ts}][{type.GetStringValue()}]: {message}");
            Console.ResetColor();
        }
    }
}

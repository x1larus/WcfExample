using System;
using WcfExample.Contracts;
using WcfExample.ServerBase.Extensions;
using WcfExample.ServerBase.OnStartup;

namespace WcfExample.Server
{
    internal class ConsoleServer
    {
        static void Main(string[] args)
        {
            var services = ServerServiceCollectionBuilder.BuildServiceCollection();
            Console.ReadKey();
        }
    }
}

using System;
using WcfExample.Server.OnStartup;

namespace WcfExample.Server
{
    internal class ConsoleServer
    {
        static void Main(string[] args)
        {
            // Дабы вкурить аче это такое стоит почитать про DependencyInjection
            // https://metanit.com/sharp/dotnet/1.1.php
            var services = ServerServiceCollectionBuilder.BuildServiceCollection();
            Console.ReadKey();
        }
    }
}

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WcfExample.ClientBase.Extensions;
using WcfExample.Contracts;

namespace WcfExample.Client
{
    class ConsoleClient
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            
            // Получаем удаленный сервис...
            var service = services.GetRemoteService<ITestService>();
            while (true)
            {
                // Вызываем удаленный сервис:)
                Console.WriteLine(service.GetHelloMessage());
                Thread.Sleep(500);
            }
            Console.ReadLine();
        }
    }
}
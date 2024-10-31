using System;
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
            var service = services.GetRemoteService<ITestService>();
            Console.WriteLine(service.GetHelloMessage());
            Console.ReadLine();
        }
    }
}
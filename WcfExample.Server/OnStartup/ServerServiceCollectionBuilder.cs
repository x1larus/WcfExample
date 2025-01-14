﻿using Microsoft.Extensions.DependencyInjection;
using WcfExample.Contracts;
using WcfExample.ServerBase.Extensions;
using WcfExample.ServerBase.Logger;
using WcfExample.Services;

namespace WcfExample.Server.OnStartup
{
    /// <summary>
    /// Сборщик сервисной коллекции
    /// </summary>
    public static class ServerServiceCollectionBuilder
    {
        /// <summary>
        /// Регистрация сервисов, которые будут вызвываться с клиента
        /// </summary>
        /// <param name="services"></param>
        private static void RegiserHostedServices(IServiceCollection services)
        {
            services.AddHostedService<ITestService, TestService>();
        }

        /// <summary>
        /// Регистрация внутренних серверных сервисов
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterLocalServices(IServiceCollection services)
        {
            services.AddSingleton<IServerLogger, ConsoleLogger>();
        }

        /// <summary>
        /// Собирает и возвращает ReadOnly коллекцию сервисов
        /// Вызывается единоразово при запуске сервера
        /// </summary>
        /// <returns>READONLY!!! cервисная коллекция</returns>
        public static IServiceCollection BuildServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            RegisterLocalServices(serviceCollection);
            RegiserHostedServices(serviceCollection);
            
            serviceCollection.MakeReadOnly();
            return serviceCollection;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfExample.Const;

namespace WcfExample.ServerBase.Extensions
{
    public static class ServerServiceCollectionExtension
    {
        /// <summary>
        /// Добавить публикуемый сервис
        /// </summary>
        /// <typeparam name="TContract">Контракт сервиса</typeparam>
        /// <typeparam name="TService">Реализация сервиса</typeparam>
        /// <param name="services"></param>
        public static void AddHostedService<TContract, TService>(this IServiceCollection services)
        {
            var contractType = typeof(TContract);
            var serviceType = typeof(TService);

            if (!serviceType.GetInterfaces().Contains(contractType))
                throw new InvalidOperationException($"Сервис {serviceType} не реализует контракт {contractType}");

            if (contractType.CustomAttributes.FirstOrDefault(el => el.AttributeType == typeof(ServiceContractAttribute)) == null)
                throw new InvalidOperationException($"Сервис {contractType} не помечен как WCF-контракт");

            services.Add(new ServiceDescriptor(contractType, serviceType, ServiceLifetime.Transient));

            if (!(Activator.CreateInstance(WcfCommunication.WcfProtocolType) is Binding protocolInstance))
                throw new InvalidOperationException($"Указанный протокол {WcfCommunication.WcfProtocolType.Name} не реализует Binding");

            var host = new ServiceHost(typeof(TService), new Uri($"{WcfCommunication.WcfConnectionString}/{contractType.Name}"));

            host.AddServiceEndpoint(typeof(TContract), protocolInstance, "");
            host.Open();
        }

        /// <summary>
        /// Получить сервис из сервисной коллекции
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="services"></param>
        public static TContract Get<TContract>(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var service = provider.GetService(typeof(TContract));
            if (!(service is TContract castedService))
                throw new InvalidOperationException($"Сервис {typeof(TContract)} не зарегистрирован в сервисной коллекции");

            return castedService;
        }
    }
}

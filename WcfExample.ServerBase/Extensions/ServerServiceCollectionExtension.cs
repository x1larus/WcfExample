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
        /// Добавить публикуемый сервис.
        /// Сервис добавляется в локальную сервисную коллекцию и публикуется
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
                throw new InvalidOperationException($"Контракт {contractType} не помечен как WCF-контракт");


            if (!(Activator.CreateInstance(WcfCommunication.WcfProtocolType) is Binding protocolInstance))
                throw new InvalidOperationException($"Указанный протокол {WcfCommunication.WcfProtocolType.Name} не реализует Binding");

            object serviceObj;

            try
            {
                serviceObj = Activator.CreateInstance(serviceType, services);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Реализация сервиса {serviceType} не имеет контруктора с параметрами IServiceCollection");
            }

            if (!(serviceObj is TService serviceInstance))
                throw new InvalidOperationException($"Не удалось создать экземпляр сервиса {serviceType}");

            services.Add(new ServiceDescriptor(contractType, serviceInstance));

            var host = new ServiceHost(serviceInstance, new Uri($"{WcfCommunication.WcfConnectionString}/{contractType.Name}"));

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

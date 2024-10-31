using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfExample.Const;

namespace WcfExample.ClientBase.Extensions
{
    public static class ClientServiceCollectionExtension
    {
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

        /// <summary>
        /// Получить удаленный сервис
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        public static TContract GetRemoteService<TContract>(this IServiceCollection services)
        {
            if(!(Activator.CreateInstance(WcfCommunication.WcfProtocolType) is Binding protocolInstance))
                throw new InvalidOperationException($"Указанный протокол {WcfCommunication.WcfProtocolType.Name} не реализует Binding");
            
            try
            {
                var factory = new ChannelFactory<TContract>(protocolInstance, new EndpointAddress($"{WcfCommunication.WcfConnectionString}/{typeof(TContract).Name}"));
                return factory.CreateChannel();
            }
            catch (Exception)
            {
                throw new InvalidOperationException(
                    $"Сервис {typeof(TContract)} не найден в удаленном хранилище");
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel;
using WcfExample.Contracts;
using WcfExample.ServerBase.Extensions;
using WcfExample.ServerBase.Logger;

namespace WcfExample.Services
{
    /// <summary>
    /// Имплементация обычного контракта
    /// Лежит ТОЛЬКО НА СЕРВЕРЕ, там же и исполняется
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class TestService : ITestService
    {
        private readonly IServerLogger _logger;

        public TestService(IServiceCollection services)
        {
            _logger = services.Get<IServerLogger>();
        }

        public string GetHelloMessage()
        {
            _logger.Log("уууууууууууу ошибка", LogType.Error);
            return "WCF сила grpc могила";
        }
    }
}

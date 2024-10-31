using System.ServiceModel;

namespace WcfExample.Contracts
{
    /// <summary>
    /// Пример контракта
    /// Лежит в Shared-сборке, то есть идет и на клиент, и на сервер
    /// </summary>
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetHelloMessage();
    }
}

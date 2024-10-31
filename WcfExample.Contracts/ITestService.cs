using System.ServiceModel;

namespace WcfExample.Contracts
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetHelloMessage();
    }
}

using WcfExample.Contracts;

namespace WcfExample.Services
{
    public class TestService : ITestService
    {
        public string GetHelloMessage()
        {
            return "WCF сила grpc могила";
        }
    }
}

using System;
using System.ServiceModel;

namespace WcfExample.Const
{
    /// <summary>
    /// Описание WCF-коммуникаций
    /// </summary>
    public static class WcfCommunication
    {
        private const string WcfProtocol = "net.tcp";
        private const string ServerAddress = "127.0.0.1";
        private const string ServerPort = "29000";
        private const string WcfServicePrefix = "WcfServices";
        
        public static readonly Type WcfProtocolType = typeof(NetTcpBinding);
        public static readonly string WcfConnectionString = $"{WcfProtocol}://{ServerAddress}:{ServerPort}/{WcfServicePrefix}";
    }
}

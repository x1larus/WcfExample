using WcfExample.Shared.Attributes;

namespace WcfExample.ServerBase.Logger
{
    public enum LogType
    {
        [StringValue("Info")]
        Info,

        [StringValue("Warning")]
        Warn,

        [StringValue("Error")]
        Error,

        [StringValue("ServiceUsage")]
        ServiceUsage
    }

    /// <summary>
    /// Логгер серверный обыкновенный одна штука
    /// </summary>
    public interface IServerLogger
    {
        void Log(string message, LogType type = LogType.Info);
    }
}

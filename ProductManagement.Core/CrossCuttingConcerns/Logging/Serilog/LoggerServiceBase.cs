using Serilog;

namespace ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog
{
    /// <summary>
    /// Base Olarak Kullanılan Sınıf Bu FileLogger, MsSqlLogger..
    /// </summary>
    public abstract class LoggerServiceBase
    {
        protected ILogger Logger;
        public void Verbose(string message) => Logger.Verbose(message);
        public void Fatal(string message) => Logger.Fatal(message);
        public void Info(string message) => Logger.Information(message);
        public void Warn(string message) => Logger.Warning(message);
        public void Debug(string message) => Logger.Debug(message);
        public void Error(string message) => Logger.Error(message);
    }
}

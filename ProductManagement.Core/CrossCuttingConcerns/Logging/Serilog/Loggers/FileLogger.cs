using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Configuration;
using ProductManagement.Core.Utilities.IoC;
using Serilog;

namespace ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        /// <summary>
        /// configuration =  Appsetting.json a ulaşmak için gerekli Servis
        /// logConfig = Apsetting.jsondan çekilen Fİlelog için gerekli ayarlalar
        /// logFilePath = Log Dosyasının kaydedileceği yol
        /// LoggerConfiguration =  Gerekli File Ayarları.
        /// </summary>
        public FileLogger()
        {
            var configuration = ServiceHelper.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration?.GetSection("SeriLog:FileLogConfiguration")
                .Get<FileLogConfiguration>() ?? throw new Exception("NullOptionsMessage");

            var logFilePath = $"{Directory.GetCurrentDirectory() + logConfig.FolderPath}.txt";

            Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }

    }
}
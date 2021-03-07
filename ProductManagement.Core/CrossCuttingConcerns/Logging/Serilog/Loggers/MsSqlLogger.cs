using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Configuration;
using ProductManagement.Core.Utilities.IoC;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{  /// <summary>
   /// configuration =  Appsetting.json a ulaşmak için gerekli Servis
   /// logConfig = Apsetting.jsondan çekilen Veritabanı Log için gerekli ayarlalar
   /// sinkOpts = Veritanında log yapılacak tablonun adı ve Otomatik oluşup oluşmayacağı
   /// seriLogConfig = Veritanında log  Gerekli Serliog ayaları  Ayarları.
   /// </summary>
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger()
        {
            var configuration = ServiceHelper.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration?.GetSection("SeriLog:MsSqlConfiguration")
                .Get<MsSqlConfiguration>() ?? throw new Exception("Utilities.Messages.SerilogMessages.NullOptionsMessage");
            var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true };

            var seriLogConfig = new LoggerConfiguration()
                .WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOpts)
                .CreateLogger();
            Logger = seriLogConfig;
        }
    }
}

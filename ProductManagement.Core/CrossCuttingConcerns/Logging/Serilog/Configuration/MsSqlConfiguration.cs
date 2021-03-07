namespace ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Configuration
{
    /// <summary>
    /// Serilog Veritabanı Log için Appsetting.jsonda bulunan ayaları set etmek için Sınıf
    /// </summary>
    public class MsSqlConfiguration
    {
        public string ConnectionString { get; set; }
    }
}

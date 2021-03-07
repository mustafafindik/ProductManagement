namespace ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Configuration
{
    /// <summary>
    /// Serilog DosyaLog için Appsetting.jsonda bulunan ayaları set etmek için Sınıf
    /// </summary>
    public class FileLogConfiguration
    {
        public string FolderPath { get; set; }
    }
}

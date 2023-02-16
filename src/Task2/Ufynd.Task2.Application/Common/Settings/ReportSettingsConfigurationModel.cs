namespace Ufynd.Task2.Application.Common.Settings
{
    public class ReportSettingsConfigurationModel
    {
        public const string NAME = "ReportSettings";
        public int Interval { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string SmtpClientHost { get; set; }
        public int SmtpClientPort { get; set; }
    }
}

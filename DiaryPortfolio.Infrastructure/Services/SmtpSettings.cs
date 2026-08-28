namespace DiaryPortfolio.Infrastructure.Services
{
    public class SmtpSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool UseSsl { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}

namespace Skillhouse.HRportal.Common
{
    public class AppSettings
    {
        public string SkillhouseDbConnectionString { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public LoginSettings LoginSettings { get; set; }
        public TokenSettings TokenSettings { get; set; }
    }
    public class TokenSettings
    {
        public int SessionExpiryInMinutes { get; set; }
        public int ShortExpiryInMinutes { get; set; }
        public int LongExpiryInMinutes { get; set; }
    }
    public class LoginSettings
    {
        public int MaxRetryCount { get; set; }
    }
}

namespace UserManagementApp.Configurations
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int JwtExpirationMinutes { get; set; }
    }
}

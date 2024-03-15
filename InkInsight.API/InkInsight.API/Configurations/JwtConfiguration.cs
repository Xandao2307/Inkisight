namespace InkInsight.API.Configurations
{
    public class JwtConfiguration
    {
        private readonly IConfiguration _configuration;
        public string JwtSecret { get; set; }
        public JwtConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            JwtSecret = _configuration.GetValue<string>("JwtSecret");
        }
        public JwtConfiguration()
        {
        }
    }
}

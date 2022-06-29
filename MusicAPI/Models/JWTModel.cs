namespace MusicAPI.Models
{
    public class ApiKeyAuthenticationDefaults
    {
        public const string AuthenticationScheme = "ApiKey";
        public const string HeaderName = "Authorization";
    }
    public class JwtAuthenticationDefaults
    {
        public const string AuthenticationScheme = "JWT";
        public const string HeaderName = "Authorization";
        public const string BearerPrefix = "Bearer";
    }
}

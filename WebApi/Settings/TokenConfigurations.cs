namespace WebApi.Settings
{
    public sealed class TokenConfigurations
    {
        public string[] Audience { get; set; }
        public string[] Issuer { get; set; }
        public int Hours { get; set; }
    }
}

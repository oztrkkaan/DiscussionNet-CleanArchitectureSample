namespace DiscussionNet.Infrastructure.Token.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiresIn { get; set; }
        public string SecurityKey { get; set; }
    }
}

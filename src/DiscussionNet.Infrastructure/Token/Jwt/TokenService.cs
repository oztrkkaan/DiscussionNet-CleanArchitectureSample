using DiscussionNet.Application.Common.Identity;
using DiscussionNet.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace DiscussionNet.Infrastructure.Token.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        private ClaimManager _claimManager;
        public TokenService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DiscussionNet.WebApi"))
                .AddJsonFile("appsettings.json")
                .Build();
            _tokenOptions = _configuration.GetSection("Jwt:TokenOptions").Get<TokenOptions>();
            _claimManager = new ClaimManager();
        }

        public Application.Common.Security.Token CreateAccessToken(int expiresInSecond, AuthenticatedUser authUser)
        {
            Application.Common.Security.Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpirationDate = DateTime.Now.AddSeconds(expiresInSecond);

            JwtSecurityToken securityToken = new(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: token.ExpirationDate,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: _claimManager.GetUserClaims(authUser));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] numbers = new byte[32];
            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(numbers);
            return Convert.ToBase64String(numbers);
        }
    }
}

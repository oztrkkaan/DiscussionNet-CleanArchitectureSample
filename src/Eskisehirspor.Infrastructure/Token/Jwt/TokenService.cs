using Eskisehirspor.Application.Common.Identity;
using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Application.Common.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Eskisehirspor.Infrastructure.Authentication.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        private ClaimManager _claimManager;
        public TokenService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Eskisehirspor.Api"))
                .AddJsonFile("appsettings.json")
                .Build();
            _tokenOptions = _configuration.GetSection("Jwt:TokenOptions").Get<TokenOptions>();
            _claimManager = new ClaimManager();
        }

        public Token CreateAccessToken(int expiresInSecond, AuthenticatedUser authUser)
        {
            Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpirationDate = DateTime.Now.AddSeconds(expiresInSecond);

            JwtSecurityToken securityToken = new(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: token.ExpirationDate,
                notBefore: DateTime.Now, //Token üretildikten ne kadar süre sonra devreye girsin
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

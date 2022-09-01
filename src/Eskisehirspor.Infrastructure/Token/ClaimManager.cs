using Eskisehirspor.Domain.Entities;
using System.Security.Claims;

namespace Eskisehirspor.Infrastructure.Authentication
{
    public class ClaimManager
    {
        private static void SetClaim(ICollection<Claim> claims, string claimName, string value)
        {
            claims.Add(new Claim(claimName, value));
        }

        public ICollection<Claim> GetUserClaims(TokenUser authenticatedUser)
        {
            var claims = new List<Claim>();

            SetClaim(claims, nameof(authenticatedUser.Username), authenticatedUser.Username);
            SetClaim(claims, nameof(authenticatedUser.Email), authenticatedUser.Email);
            SetClaim(claims, nameof(authenticatedUser.DisplayName), authenticatedUser.DisplayName);
            SetClaim(claims, nameof(authenticatedUser.ExpirationDate), authenticatedUser.ExpirationDate.ToString());
            SetClaim(claims, nameof(authenticatedUser.Roles), authenticatedUser.Roles);

            return claims;
        }
    }
}

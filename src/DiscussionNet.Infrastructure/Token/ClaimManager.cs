using DiscussionNet.Application.Common.Identity;
using System.Security.Claims;

namespace DiscussionNet.Infrastructure.Token
{
    public class ClaimManager
    {
        private static void SetClaim(ICollection<Claim> claims, string claimName, string value)
        {
            claims.Add(new Claim(claimName, value));
        }

        public ICollection<Claim> GetUserClaims(AuthenticatedUser authenticatedUser)
        {
            var claims = new List<Claim>();
            SetClaim(claims, nameof(authenticatedUser.Id), authenticatedUser.Id.ToString());
            SetClaim(claims, nameof(authenticatedUser.Username), authenticatedUser.Username);
            SetClaim(claims, nameof(authenticatedUser.Email), authenticatedUser.Email);
            SetClaim(claims, nameof(authenticatedUser.DisplayName), authenticatedUser.DisplayName);
            SetClaim(claims, ClaimTypes.Role, string.Join(',', authenticatedUser.Roles));

            return claims;
        }
    }
}

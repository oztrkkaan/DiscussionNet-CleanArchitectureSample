using System.Security.Claims;
using System.Security.Principal;

namespace Eskisehirspor.Application.Common.Identity
{
    public static class IdentityExtension
    {

        private static T GetValue<T>(IIdentity identity, string claimType)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(claimType);
            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }

        public static int GetUserId(this IIdentity identity) => GetValue<int>(identity, "Id");
        public static string GetUsername(this IIdentity identity) => GetValue<string>(identity, "Username");
        public static string GetDisplayName(this IIdentity identity) => GetValue<string>(identity, "DisplayName");
        public static string GetEmail(this IIdentity identity) => GetValue<string>(identity, "Email");
        public static List<string> GetRoles(this IIdentity identity) => GetValue<string>(identity, ClaimTypes.Role).Split(',').ToList();
    }
}

using DiscussionNet.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace DiscussionNet.Application.Common.Identity
{
    public class IdentityManager : IIdentityManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentity _identity;
        public IdentityManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _identity = _httpContextAccessor.HttpContext.User.Identity;
        }
        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        public AuthenticatedUser User => GetUser();

        private AuthenticatedUser GetUser()
        {
            return new AuthenticatedUser
            {
                Id = _identity.GetUserId(),
                DisplayName = _identity.GetDisplayName(),
                Email = _identity.GetEmail(),
                Roles = _identity.GetRoles(),
                Username = _identity.GetUsername()
            };
        }
    }
}

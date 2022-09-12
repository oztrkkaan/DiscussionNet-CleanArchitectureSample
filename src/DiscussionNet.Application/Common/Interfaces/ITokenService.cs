using DiscussionNet.Application.Common.Identity;
using DiscussionNet.Application.Common.Security;

namespace DiscussionNet.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Token CreateAccessToken(int expiresInSecond, AuthenticatedUser user);
        public string CreateRefreshToken();
    }
}

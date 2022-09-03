using Eskisehirspor.Application.Common.Identity;
using Eskisehirspor.Application.Common.Security;

namespace Eskisehirspor.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Token CreateAccessToken(int expiresInSecond, AuthenticatedUser user);
        public string CreateRefreshToken();
    }
}

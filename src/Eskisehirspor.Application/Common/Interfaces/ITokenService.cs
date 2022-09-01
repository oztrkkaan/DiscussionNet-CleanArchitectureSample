using Eskisehirspor.Application.Common.Security;
using Eskisehirspor.Domain.Entities;

namespace Eskisehirspor.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Token CreateAccessToken(int expiresInSecond, TokenUser user);
        public string CreateRefreshToken();
    }
}

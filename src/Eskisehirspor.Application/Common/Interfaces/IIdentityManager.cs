using Eskisehirspor.Application.Common.Identity;


namespace Eskisehirspor.Application.Common.Interfaces
{
    public interface IIdentityManager
    {
        public bool IsAuthenticated { get; }
        public AuthenticatedUser User { get; }
    }
}

using DiscussionNet.Application.Common.Identity;

namespace DiscussionNet.Application.Common.Interfaces
{
    public interface IIdentityManager
    {
        public bool IsAuthenticated { get; }
        public AuthenticatedUser User { get; }
    }
}

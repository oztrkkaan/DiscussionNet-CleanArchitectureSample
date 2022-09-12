using Microsoft.Extensions.Caching.Distributed;

namespace DiscussionNet.Application.Common.Caching.Redis
{
    public interface IRedisClientProvider
    {
        IDistributedCache GetDistributedCache();

    }
}

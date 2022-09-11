using Microsoft.Extensions.Caching.Distributed;

namespace Eskisehirspor.Application.Common.Caching.Redis
{
    public interface IRedisClientProvider
    {
        IDistributedCache GetDistributedCache();

    }
}

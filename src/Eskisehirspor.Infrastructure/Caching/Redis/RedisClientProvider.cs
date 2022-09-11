using Eskisehirspor.Application.Common.Caching.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;

namespace Eskisehirspor.Infrastructure.Caching.Redis
{
    public class RedisClientProvider : IRedisClientProvider
    {
        private readonly IRedisClientConfigProvider _redisClientConfigProvider;
        public RedisClientProvider(IRedisClientConfigProvider redisClientConfigProvider)
        {
            _redisClientConfigProvider = redisClientConfigProvider;
        }

        public IDistributedCache GetDistributedCache()
        {
            RedisConfig redisConfig = _redisClientConfigProvider.GetRedisConfig();
            IOptions<RedisCacheOptions> options = new RedisCacheOptions()
            {
                Configuration = redisConfig.Configuration,
                InstanceName = redisConfig.InstanceName
            };

            return new RedisCache(options);
        }
    }
}

using Eskisehirspor.Infrastructure.Cache.Redis.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;

namespace Eskisehirspor.Infrastructure.Cache.Redis
{
    public class RedisClientProvider : IRedisClientProvider
    {
        private IRedisClientConfigProvider redisClientConfigProvider { get; set; }
        public RedisClientProvider(IRedisClientConfigProvider redisClientConfigProvider)
        {
            this.redisClientConfigProvider = redisClientConfigProvider;
        }

        public IRedisClient GetRedisClient(string redisName)
        {
            return new RedisClient(GetDistributedCache(redisName));
        }

        public IDistributedCache GetDistributedCache(string redisName)
        {
            RedisConfig redisConfig = redisClientConfigProvider.GetRedisConfig(redisName);
            IOptions<RedisCacheOptions> options = new RedisCacheOptions()
            {
                Configuration = redisConfig.Configuration,
                InstanceName = redisConfig.InstanceName
            };

            return new RedisCache(options);
        }
    }
}

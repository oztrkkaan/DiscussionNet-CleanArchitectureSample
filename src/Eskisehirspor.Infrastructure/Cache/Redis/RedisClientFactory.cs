using Eskisehirspor.Infrastructure.Cache.Redis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.Redis
{
    public class RedisClientFactory : IRedisClientFactory
    {
        private readonly IRedisClientProvider redisClientProvider;

        public RedisClientFactory(IRedisClientProviderFactory redisClientProviderFactory)
        {
            redisClientProvider = redisClientProviderFactory.GetRedisClientProvider();
        }
        public IRedisClient GetRedisClient(string redisName)
        {
            return redisClientProvider.GetRedisClient(redisName);
        }
    }
}

using Eskisehirspor.Infrastructure.Cache.Redis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.Redis
{
    public class RedisClientProviderFactory : IRedisClientProviderFactory
    {
        private IRedisClientConfigProvider redisClientConfigProvider { get; set; }
        public RedisClientProviderFactory(IRedisClientConfigProvider redisClientConfigProvider)
        {
            this.redisClientConfigProvider = redisClientConfigProvider;
        }
        public IRedisClientProvider GetRedisClientProvider()
        {
            return new RedisClientProvider(redisClientConfigProvider);
        }

    }
}

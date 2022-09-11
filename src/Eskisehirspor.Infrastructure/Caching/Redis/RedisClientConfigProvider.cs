using Eskisehirspor.Application.Common.Caching.Redis;
using Microsoft.Extensions.Configuration;

namespace Eskisehirspor.Infrastructure.Caching.Redis
{
    public class RedisClientConfigProvider : IRedisClientConfigProvider
    {
        IConfiguration _configuration;
        public RedisClientConfigProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public RedisConfig GetRedisConfig()
        {
            RedisConfig redisConfig = new();

            string host = $"{_configuration[$"Redis:Hostname"]}";
            string port = $"{_configuration[$"Redis:Port"]}";
            string password = $"{_configuration[$"Redis:Password"]}";
            string instanceName = $"{_configuration[$"Redis:InstanceName"]}";
            redisConfig.Configuration = $"{host}:{port}";

            if (!string.IsNullOrEmpty(password))
            {
                redisConfig.Configuration += $",password={password}";
            }

            redisConfig.InstanceName = instanceName;

            return redisConfig;
        }
    }
}

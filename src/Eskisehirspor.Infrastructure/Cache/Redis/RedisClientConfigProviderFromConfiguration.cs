using Eskisehirspor.Infrastructure.Cache.Redis.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Eskisehirspor.Infrastructure.Cache.Redis
{
    public class RedisClientConfigProviderFromConfiguration : IRedisClientConfigProvider
    {
        private readonly IConfiguration _configuration;


        public RedisClientConfigProviderFromConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public RedisConfig GetRedisConfig(string redisName)
        {
            RedisConfig redisConfig = new RedisConfig();
            string host = $"{_configuration[$"{redisName}:host"]}";
            string port = $"{_configuration[$"{redisName}:port"]}";
            string password = $"{_configuration[$"{redisName}:password"]}";
            redisConfig.Configuration = $"{host}:{port}";

            if (!string.IsNullOrEmpty(password))
            {
                redisConfig.Configuration += $",password={password}";
            }
            string instanceName = $"{_configuration[$"{redisName}:instance-name"]}";
            redisConfig.InstanceName = instanceName;

            return redisConfig;
        }
    }
}

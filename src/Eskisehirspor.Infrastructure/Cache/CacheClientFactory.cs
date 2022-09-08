using Eskisehirspor.Infrastructure.Cache.InMemory;
using Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces;
using Eskisehirspor.Infrastructure.Cache.Redis;
using Eskisehirspor.Infrastructure.Cache.Redis.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Eskisehirspor.Infrastructure.Cache
{
    public class CacheClientFactory
    {
        private static readonly object lockObject = new object();
        private static IConfigurationRoot configuration;
        static CacheClientFactory()
        {
            lock (lockObject)
            {
                Initialize();
                if (RedisClient == null)
                {
                    IRedisClientConfigProvider redisClientConfigProvider = new RedisClientConfigProviderFromConfiguration(configuration);

                    IRedisClientProviderFactory redisClientProviderFactory = new RedisClientProviderFactory(redisClientConfigProvider);

                    IRedisClientFactory redisClientFactory = new RedisClientFactory(redisClientProviderFactory);
                    RedisClient = redisClientFactory.GetRedisClient("redis");
                }

                if (InMemoryClient == null)
                {
                    IInMemoryClientProviderFactory inMemoryClientProviderFactory = new InMemoryClientProviderFactory();
                    IInMemoryClientFactory inMemoryClientFactory = new InMemoryClientFactory(inMemoryClientProviderFactory);
                    InMemoryClient = inMemoryClientFactory.GetInMemoryClient();
                }

            }
        }
        static void Initialize()
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();


        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(LoggerFactory.Create(builder => builder.AddConsole()));
            serviceCollection.AddLogging();
        }
        public static IRedisClient RedisClient { get; private set; } = null;

        public static IInMemoryClient InMemoryClient { get; private set; } = null;
    }
}

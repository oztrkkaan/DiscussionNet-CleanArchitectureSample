using Eskisehirspor.Application.Common.Caching.Redis;
using Eskisehirspor.Application.Common.Hangfire;
using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Infrastructure.Caching.Redis;
using Eskisehirspor.Infrastructure.Email;
using Eskisehirspor.Infrastructure.Hangfire;
using Eskisehirspor.Infrastructure.Token.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Eskisehirspor.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IHangfireConfiguration, HangfireConfiguration>();
            services.AddTransient<IRedisClientConfigProvider, RedisClientConfigProvider>();
            services.AddTransient<IRedisClientProvider, RedisClientProvider>();
            services.AddSingleton<IRedisClient, RedisClient>();

        }
    }
}

using DiscussionNet.Application.Common.Caching.Redis;
using DiscussionNet.Application.Common.Hangfire;
using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Infrastructure.Caching.Redis;
using DiscussionNet.Infrastructure.Email;
using DiscussionNet.Infrastructure.Hangfire;
using DiscussionNet.Infrastructure.Token.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionNet.Infrastructure
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

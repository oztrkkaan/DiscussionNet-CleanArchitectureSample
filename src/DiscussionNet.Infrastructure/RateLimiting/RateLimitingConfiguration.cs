using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionNet.Infrastructure.RateLimiting
{
    public static class RateLimitingConfiguration
    {
        public static void AddRateLimiting(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddOptions();
            service.AddMemoryCache();
            service.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            service.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            service.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            service.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            service.AddHttpContextAccessor();
            service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            service.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureIpRateLimiting(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseIpRateLimiting();
        }
    }
}

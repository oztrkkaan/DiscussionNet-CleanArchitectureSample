using Microsoft.Extensions.DependencyInjection;

namespace DiscussionNet.Infrastructure.Utilities.IoC
{
    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}

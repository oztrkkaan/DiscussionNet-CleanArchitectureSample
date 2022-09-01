using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Infrastructure.Authentication.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Eskisehirspor.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<ITokenService,TokenService>();
        }
    }
}

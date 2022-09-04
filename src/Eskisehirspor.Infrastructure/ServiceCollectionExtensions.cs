using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Infrastructure.Email;
using Eskisehirspor.Infrastructure.Token.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Eskisehirspor.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<ITokenService,TokenService>();
            services.AddTransient<IMailService, MailService>();
        }
    }
}

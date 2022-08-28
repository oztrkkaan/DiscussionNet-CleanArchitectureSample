using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eskisehirspor.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ForumDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IForumDbContext>(provider => provider.GetService<ForumDbContext>());
        }
    }
}

using Eskisehirspor.Application.Common.Hangfire;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads.Publisher;
using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Eskisehirspor.Infrastructure.Hangfire
{

    public class HangfireConfiguration : IHangfireConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public HangfireConfiguration(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
              .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseRecommendedSerializerSettings()
              .UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
              .UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
              {
                  CommandBatchMaxTimeout = TimeSpan.FromMinutes(10),
                  SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                  QueuePollInterval = TimeSpan.Zero,
                  UseRecommendedIsolationLevel = true,
                  DisableGlobalLocks = true,
              }));

            services.AddHangfireServer();
        }
        public void ConfigureDashboard(IApplicationBuilder appBuilder)
        {
            appBuilder.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Panel / Hangfire Dashboard",
                Authorization = new[]
             {
                 new HangfireCustomBasicAuthenticationFilter{
                     User = _configuration.GetSection("Hangfire:Username").Value,
                     Pass = _configuration.GetSection("Hangfire:Password").Value
                 }
                },
                IgnoreAntiforgeryToken = true
            });
        }

        public void InitializeJobs()
        {
            RecurringJob.AddOrUpdate<IMediator>(nameof(GetLatestTopicsPublisher), m => m.Publish(new GetLatestTopicsPublisher { }, default), Cron.Minutely());
        }
    }
}

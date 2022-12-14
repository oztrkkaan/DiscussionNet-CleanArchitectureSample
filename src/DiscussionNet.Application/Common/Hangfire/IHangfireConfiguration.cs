using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionNet.Application.Common.Hangfire
{
    public interface IHangfireConfiguration
    {
        void Configure(IServiceCollection services);
        void InitializeJobs();
        void ConfigureDashboard(IApplicationBuilder appBuilder);
    }
}

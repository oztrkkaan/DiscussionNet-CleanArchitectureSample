using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace DiscussionNet.Infrastructure.Serilog
{
    public static class SerilogConfigurationExtensions
    {
        public static void AddSerilog(this IServiceCollection service)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Map(evt => evt.Level, (level, wt) => wt.File(AppDomain.CurrentDomain.BaseDirectory+ $"/logs/{level}-.txt", rollingInterval: RollingInterval.Day))
                .CreateLogger();

            service.AddLogging(x => x.AddSerilog())
                   .BuildServiceProvider(true);
        }
    }
}

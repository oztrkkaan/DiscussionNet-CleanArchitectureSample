using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eskisehirspor.Application.UseCases.Feed.LatestThreads.Worker
{
    public class GetLatestTopicsWorker : BackgroundService
    {
        private readonly IMessageScheduler _scheduler;

        public GetLatestTopicsWorker(IServiceScopeFactory factory)
        {
            
            _scheduler = factory.CreateScope().ServiceProvider.GetRequiredService<IMessageScheduler>();
        }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _scheduler.SchedulePublish<GetLatestTopicsJob>(DateTime.UtcNow.AddSeconds(3), stoppingToken);
        }
    }
}



using MassTransit;

namespace Eskisehirspor.Application.UseCases.Feed.LatestThreads.JobConsumer
{
    public class GetLatestTopicsConsumer : IConsumer<GetLatestTopicsJob>
    {
        //IMediator _mediator;

        //public GetLatestTopicsConsumer(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        public async Task Consume(ConsumeContext<GetLatestTopicsJob> context)
        {
            Uri notificationService = new Uri("queue:get-latest-topics-job");
            await context.ScheduleSend<GetLatestTopicsJob>(notificationService,DateTime.UtcNow + TimeSpan.FromSeconds(5), new { });
        }

    }
}

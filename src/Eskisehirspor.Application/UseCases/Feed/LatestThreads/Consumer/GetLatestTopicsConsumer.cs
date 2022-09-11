using Eskisehirspor.Application.UseCases.Feed.LatestThreads.Publisher;
using MassTransit;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Feed.LatestThreads.Consumer
{
    public class GetLatestTopicsConsumer : IConsumer<GetLatestTopicsPublisher>
    {
        IMediator _mediator;
        public GetLatestTopicsConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetLatestTopicsPublisher> context)
        {
            await _mediator.Publish(new GetLatestTopicsEvent { });
        }
    }
}

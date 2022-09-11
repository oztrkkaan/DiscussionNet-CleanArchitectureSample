using Eskisehirspor.Application.UseCases.Feed.RefreshLatestTopics.Publisher;
using MassTransit;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Feed.RefreshLatestTopics.Consumer
{
    public class RefreshLatestTopicsConsumer : IConsumer<RefreshLatestTopicsPublisher>
    {
        IMediator _mediator;
        public RefreshLatestTopicsConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RefreshLatestTopicsPublisher> context)
        {
            await _mediator.Publish(new RefreshLatestTopicsEvent { });
        }
    }
}

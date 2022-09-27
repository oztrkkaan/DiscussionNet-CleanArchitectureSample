using DiscussionNet.Application.UseCases.Notification.ReactionNotification.Publisher;
using MassTransit;
using MediatR;

namespace DiscussionNet.Application.UseCases.Notification.ReactionNotification.Consumer
{
    public record ReactionNotificationConsumer : IConsumer<ReactionNotificationPublisher>
    {
        private readonly IMediator _mediator;

        public ReactionNotificationConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ReactionNotificationPublisher> context)
        {
            await _mediator.Publish(new ReactionNotificationEvent
            {
                ReactedUserId = context.Message.ReactedUserId,
                ReceiverUserId = context.Message.ReceiverUserId,
                ThreadId = context.Message.ThreadId
            });
        }
    }
}

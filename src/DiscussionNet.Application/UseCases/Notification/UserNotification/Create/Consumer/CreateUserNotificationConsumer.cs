using DiscussionNet.Application.UseCases.Notification.UserNotification.Create.Publisher;
using MassTransit;
using MediatR;

namespace DiscussionNet.Application.UseCases.Notification.UserNotification.Create.Consumer
{
    public class CreateUserNotificationConsumer : IConsumer<CreateUserNotificationPublisher>
    {
        IMediator _mediator;

        public CreateUserNotificationConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<CreateUserNotificationPublisher> context)
        {
            await _mediator.Publish(new CreateUserNotificationEvent
            {
                NotificationId = context.Message.NotificationId,
                UserId = context.Message.UserId
            });
        }
    }
}

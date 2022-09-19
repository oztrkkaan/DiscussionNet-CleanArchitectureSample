using MassTransit;
using MediatR;


namespace DiscussionNet.Application.UseCases.Notification.UserNotification.Create.Publisher
{
    public record CreateUserNotificationPublisher : INotification
    {
        public int NotificationId { get; init; }
        public int UserId { get; init; }
    }
    public class CreateUserNotificationPublisherHandler : INotificationHandler<CreateUserNotificationPublisher>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string QUEUE_NAME = "notificationservice.create-user-notifications";
        public CreateUserNotificationPublisherHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Handle(CreateUserNotificationPublisher notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

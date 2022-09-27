using DiscussionNet.Application.Common.Interfaces;
using MassTransit;
using MediatR;

namespace DiscussionNet.Application.Features.Notification.ReactionNotification.Publisher
{
    public record ReactionNotificationPublisher : INotification
    {
        public int ReactedUserId { get; set; }
        public int ReceiverUserId { get; init; }
        public int ThreadId { get; init; }
    }

    internal class ReactionNotificationPublisherHandler : INotificationHandler<ReactionNotificationPublisher>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string EMAILSERVICE_QUEUE_NAME = "notificationservice.reaction-notifications";
        public ReactionNotificationPublisherHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(ReactionNotificationPublisher notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{EMAILSERVICE_QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

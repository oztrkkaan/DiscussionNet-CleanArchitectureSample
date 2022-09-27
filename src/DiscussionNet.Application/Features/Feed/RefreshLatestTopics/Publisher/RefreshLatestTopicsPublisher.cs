using MassTransit;
using MediatR;

namespace DiscussionNet.Application.UseCases.Feed.RefreshLatestTopics.Publisher
{
    public class RefreshLatestTopicsPublisher : INotification
    { }
    public class RefreshLatestTopicsPublisherHandler : INotificationHandler<RefreshLatestTopicsPublisher>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string QUEUE_NAME = "jobs.refresh-latest-topics";
        public RefreshLatestTopicsPublisherHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Handle(RefreshLatestTopicsPublisher notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

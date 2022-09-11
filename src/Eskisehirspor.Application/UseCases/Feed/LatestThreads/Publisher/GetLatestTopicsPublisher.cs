using MassTransit;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Feed.LatestThreads.Publisher
{
    public class GetLatestTopicsPublisher : INotification 
    { }
    public class GetLatestTopicsPublisherHandler : INotificationHandler<GetLatestTopicsPublisher>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string QUEUE_NAME = "jobs.get-latest-topics";
        public GetLatestTopicsPublisherHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(GetLatestTopicsPublisher notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

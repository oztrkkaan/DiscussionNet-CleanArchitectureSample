using MassTransit;
using MediatR;

namespace DiscussionNet.Application.UseCases.Email.RegistrationEmail.Publisher
{
    public class SendRegistrationEmailPublisher : INotification
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }

    public class SendRegistrationEmailPublisherHandler : INotificationHandler<SendRegistrationEmailPublisher>
    {
        ISendEndpointProvider _sendEndpointProvider;
        private const string EMAILSERVICE_QUEUE_NAME = "emailservice.registration";
        public SendRegistrationEmailPublisherHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(SendRegistrationEmailPublisher notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{EMAILSERVICE_QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

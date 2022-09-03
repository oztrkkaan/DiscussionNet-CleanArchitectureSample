using MassTransit;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Email.RegistrationEmail.Publisher
{
    public class SendRegistrationEmailEvent : INotification
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }

    public class SendRegistrationEmailEventHandler : INotificationHandler<SendRegistrationEmailEvent>
    {
        ISendEndpointProvider _sendEndpointProvider;
        private const string EMAILSERVICE_QUEUE_NAME = "emailservice.registration";
        public SendRegistrationEmailEventHandler(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(SendRegistrationEmailEvent notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{EMAILSERVICE_QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

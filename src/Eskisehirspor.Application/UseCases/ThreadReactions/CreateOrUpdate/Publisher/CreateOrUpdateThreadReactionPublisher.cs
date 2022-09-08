using Eskisehirspor.Application.Common.Interfaces;
using MassTransit;
using MediatR;
using static Eskisehirspor.Domain.Entities.ThreadReaction;

namespace Eskisehirspor.Application.UseCases.ThreadReactions.CreateOrUpdate.Publisher
{
    public class CreateOrUpdateThreadReactionPublisher : INotification
    {
        public Reactions Reaction { get; set; }
        public int ThreadId { get; set; }
        public int? ReactedUserId { get; set; }
    }

    public class CreateThreadReactionPublisherHandler : INotificationHandler<CreateOrUpdateThreadReactionPublisher>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IIdentityManager _identityManager;
        private const string EMAILSERVICE_QUEUE_NAME = "reactionservice.reaction";
        public CreateThreadReactionPublisherHandler(ISendEndpointProvider sendEndpointProvider, IIdentityManager identityManager)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _identityManager = identityManager;
        }

        public async Task Handle(CreateOrUpdateThreadReactionPublisher notification, CancellationToken cancellationToken)
        {
            notification.ReactedUserId = _identityManager.User.Id;

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{EMAILSERVICE_QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

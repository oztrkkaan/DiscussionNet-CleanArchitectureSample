using DiscussionNet.Application.Common.Interfaces;
using MassTransit;
using MediatR;
using static DiscussionNet.Domain.Entities.CommentReaction;

namespace DiscussionNet.Application.Features.CommentReactions.CreateOrUpdate.Publisher
{
    public class CreateOrUpdateCommentReactionPublisher : INotification
    {
        public Reactions Reaction { get; set; }
        public int CommentId { get; set; }
        public int? ReactedUserId { get; set; }
    }

    public class CreateCommentReactionPublisherHandler : INotificationHandler<CreateOrUpdateCommentReactionPublisher>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string EMAILSERVICE_QUEUE_NAME = "reactionservice.reaction";
        public CreateCommentReactionPublisherHandler(ISendEndpointProvider sendEndpointProvider, IIdentityManager identityManager)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(CreateOrUpdateCommentReactionPublisher notification, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{EMAILSERVICE_QUEUE_NAME}"));
            await endpoint.Send(notification, cancellationToken);
        }
    }
}

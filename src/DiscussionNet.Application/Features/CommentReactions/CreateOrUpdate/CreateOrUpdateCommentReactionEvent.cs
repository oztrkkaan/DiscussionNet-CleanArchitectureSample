using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.Notification.ReactionNotification.Publisher;
using DiscussionNet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DiscussionNet.Domain.Entities.CommentReaction;


namespace DiscussionNet.Application.Features.CommentReactions.CreateOrUpdate
{
    public record CreateOrUpdateCommentReactionEvent : INotification
    {
        public Reactions Reaction { get; init; }
        public int CommentId { get; init; }
        public int ReactedUserId { get; set; }
    }

    public class CreateOrUpdateCommentReactionEventHandler : INotificationHandler<CreateOrUpdateCommentReactionEvent>
    {
        private readonly IDiscussionDbContext _context;
        private readonly IMediator _mediator;

        public CreateOrUpdateCommentReactionEventHandler(IDiscussionDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(CreateOrUpdateCommentReactionEvent notification, CancellationToken cancellationToken)
        {
            var comment = GetCommentById(notification.CommentId);
            var reactedUser = _context.Users.SingleOrDefault(m => m.Id == notification.ReactedUserId);

            await CreateOrUpdate(notification.Reaction, comment, reactedUser, cancellationToken);
        }

        public Domain.Entities.Comment GetCommentById(int commentId)
        {
            return _context.Comments.Include(m=>m.User).SingleOrDefault(m => m.Id == commentId);
        }
        public CommentReaction GetExistReaction(int commentId, int userId)
        {
            return _context.CommentReactions.SingleOrDefault(m => m.Comment.Id == commentId && m.ReactedBy.Id == userId);
        }

        public async Task CreateOrUpdate(Reactions reaction, Domain.Entities.Comment comment, Domain.Entities.User reactedUser, CancellationToken cancellationToken)
        {
            var existReaction = GetExistReaction(comment.Id, reactedUser.Id);
            if (existReaction is null)
            {
                var newReaction = new CommentReaction(comment, reaction, reactedUser);
                await _context.CommentReactions.AddAsync(newReaction, cancellationToken);
                await SendReactionNotification(comment.User.Id, reactedUser.Id, comment.Id);
            }
            else
            {
                existReaction.SetReaction(reaction, true);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SendReactionNotification(int receiverUserId, int reactedUserId, int commentId)
        {
            await _mediator.Publish(new ReactionNotificationPublisher
            {
                ReceiverUserId = receiverUserId,
                CommentId = commentId,
                ReactedUserId = reactedUserId
            });
        }
    }
}

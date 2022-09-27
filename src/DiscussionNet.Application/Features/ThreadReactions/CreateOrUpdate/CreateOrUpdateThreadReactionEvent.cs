using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.UseCases.Notification.ReactionNotification.Publisher;
using DiscussionNet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DiscussionNet.Domain.Entities.ThreadReaction;


namespace DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate
{
    public record CreateOrUpdateThreadReactionEvent : INotification
    {
        public Reactions Reaction { get; init; }
        public int ThreadId { get; init; }
        public int ReactedUserId { get; set; }
    }

    public class CreateOrUpdateThreadReactionEventHandler : INotificationHandler<CreateOrUpdateThreadReactionEvent>
    {
        private readonly IForumDbContext _context;
        private readonly IMediator _mediator;

        public CreateOrUpdateThreadReactionEventHandler(IForumDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(CreateOrUpdateThreadReactionEvent notification, CancellationToken cancellationToken)
        {
            var thread = GetThreadById(notification.ThreadId);
            var reactedUser = _context.Users.SingleOrDefault(m => m.Id == notification.ReactedUserId);

            await CreateOrUpdate(notification.Reaction, thread, reactedUser, cancellationToken);
        }

        public Domain.Entities.Thread GetThreadById(int threadId)
        {
            return _context.Threads.Include(m=>m.User).SingleOrDefault(m => m.Id == threadId);
        }
        public ThreadReaction GetExistReaction(int threadId, int userId)
        {
            return _context.ThreadReactions.SingleOrDefault(m => m.Thread.Id == threadId && m.ReactedBy.Id == userId);
        }

        public async Task CreateOrUpdate(Reactions reaction, Domain.Entities.Thread thread, Domain.Entities.User reactedUser, CancellationToken cancellationToken)
        {
            var existReaction = GetExistReaction(thread.Id, reactedUser.Id);
            if (existReaction is null)
            {
                var newReaction = new ThreadReaction(thread, reaction, reactedUser);
                await _context.ThreadReactions.AddAsync(newReaction, cancellationToken);
                await SendReactionNotification(thread.User.Id, reactedUser.Id, thread.Id);
            }
            else
            {
                existReaction.SetReaction(reaction, true);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SendReactionNotification(int receiverUserId, int reactedUserId, int threadId)
        {
            await _mediator.Publish(new ReactionNotificationPublisher
            {
                ReceiverUserId = receiverUserId,
                ThreadId = threadId,
                ReactedUserId = reactedUserId
            });
        }
    }
}

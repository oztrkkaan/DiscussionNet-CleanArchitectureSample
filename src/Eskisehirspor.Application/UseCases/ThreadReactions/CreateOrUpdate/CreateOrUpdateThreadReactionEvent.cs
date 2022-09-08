using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Domain.Entities;
using MediatR;
using static Eskisehirspor.Domain.Entities.ThreadReaction;


namespace Eskisehirspor.Application.UseCases.ThreadReactions.CreateOrUpdate
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

        public CreateOrUpdateThreadReactionEventHandler(IForumDbContext context, IIdentityManager identityManager)
        {
            _context = context;
        }

        public async Task Handle(CreateOrUpdateThreadReactionEvent notification, CancellationToken cancellationToken)
        {
            var thread = GetThreadById(notification.ThreadId);
            var user = _context.Users.SingleOrDefault(m => m.Id == notification.ReactedUserId);

            await CreateOrUpdate(notification.Reaction, thread, user, cancellationToken);
        }

        public Domain.Entities.Thread GetThreadById(int threadId)
        {
            return _context.Threads.SingleOrDefault(m => m.Id == threadId);
        }
        public ThreadReaction GetExistReaction(int threadId, int userId)
        {
            return _context.ThreadReactions.SingleOrDefault(m => m.Thread.Id == threadId && m.ReactedBy.Id == userId);
        }

        public async Task CreateOrUpdate(Reactions reaction, Domain.Entities.Thread thread, Domain.Entities.User user, CancellationToken cancellationToken)
        {
            var existReaction = GetExistReaction(thread.Id, user.Id);
            if (existReaction is null)
            {
                var newReaction = new ThreadReaction(thread, reaction, user);

                await _context.ThreadReactions.AddAsync(newReaction, cancellationToken);
            }
            else
            {
                existReaction.SetReaction(reaction, true);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

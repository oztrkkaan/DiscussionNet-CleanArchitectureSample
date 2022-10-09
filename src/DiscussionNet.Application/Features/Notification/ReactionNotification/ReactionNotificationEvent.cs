using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.User.GetUserById;
using GuardNet;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace DiscussionNet.Application.Features.Notification.ReactionNotification
{
    internal record ReactionNotificationEvent : INotification
    {
        public int ReactedUserId { get; init; }
        public int ReceiverUserId { get; init; }
        public int ThreadId { get; init; }
    }

    internal class ReactionNotificationEventHandler : INotificationHandler<ReactionNotificationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IDiscussionDbContext _context;

        public ReactionNotificationEventHandler(IMediator mediator, IDiscussionDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }
        private const string NOTIFICATION_CONTENT = "<b>{0}</b> adlı kullanıcı <b>{1}</b> başlığındaki yazınızı beğendi.";
        public async Task Handle(ReactionNotificationEvent notification, CancellationToken cancellationToken)
        {
            var reactedUser = await _mediator.Send(new GetUserByIdQuery { UserId = notification.ReactedUserId }, cancellationToken);
            ArgumentNullException.ThrowIfNull(reactedUser);

            var thread = _context.Threads.Include(m => m.Topic).FirstOrDefault(m => m.Id == notification.ThreadId);
            ArgumentNullException.ThrowIfNull(thread);

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var notificationId = await CreateNotification(reactedUser.Username, thread.Topic.Subject, cancellationToken);
            await CreateUserNotification(notification.ReceiverUserId, notificationId, cancellationToken);

            transaction.Complete();
        }

        public async Task<int> CreateNotification(string username, string subject, CancellationToken cancellationToken)
        {
            string content = string.Format(NOTIFICATION_CONTENT, username, subject);
            string url = "#link";

            var notification = new Domain.Entities.Notification(content, url);

            await _context.Notifications.AddAsync(notification, cancellationToken);
            int effectedRows = await _context.SaveChangesAsync(cancellationToken);
            Guard.NotEqualTo(effectedRows, 1, new ArgumentException("CreateNotification failed."));

            return notification.Id;
        }

        public async Task CreateUserNotification(int userId, int notificationId, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == userId);
            var notification = _context.Notifications.FirstOrDefault(m => m.Id == notificationId);
            var userNotification = new Domain.Entities.UserNotification(user, notification);

            await _context.UserNotifications.AddAsync(userNotification, cancellationToken);
            int effectedRows = await _context.SaveChangesAsync(cancellationToken);
            Guard.NotEqualTo(effectedRows, 1, new ArgumentException("CreateUserNotification failed."));
        }
    }
}

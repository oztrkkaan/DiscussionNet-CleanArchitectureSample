using DiscussionNet.Application.Common.Interfaces;
using MediatR;

namespace DiscussionNet.Application.UseCases.Notification.UserNotification.Create
{
    internal record CreateUserNotificationEvent : INotification
    {
        public int NotificationId { get; init; }
        public int UserId { get; init; }
    }
    internal class CreateUserNotificationEventHandler : INotificationHandler<CreateUserNotificationEvent>
    {
        IForumDbContext _context;
        public CreateUserNotificationEventHandler(IForumDbContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateUserNotificationEvent eventNotification, CancellationToken cancellationToken)
        {
                var user = _context.Users.FirstOrDefault(m => m.Id == eventNotification.UserId);
                var notification = _context.Notifications.FirstOrDefault(m => m.Id == eventNotification.NotificationId);
                var userNotification = new Domain.Entities.UserNotification(user, notification);

                await _context.UserNotifications.AddAsync(userNotification, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

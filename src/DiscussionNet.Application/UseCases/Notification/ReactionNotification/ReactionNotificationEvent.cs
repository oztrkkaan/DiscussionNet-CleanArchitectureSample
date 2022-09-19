using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.UseCases.Notification.CreateNotification;
using DiscussionNet.Application.UseCases.Notification.UserNotification.Create.Publisher;
using DiscussionNet.Application.UseCases.User.GetUserById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace DiscussionNet.Application.UseCases.Notification.ReactionNotification
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
        private readonly IForumDbContext _context;

        public ReactionNotificationEventHandler(IMediator mediator, IForumDbContext context)
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

            var newNotification = await CreateNotification(reactedUser.Username, thread.Topic.Subject, cancellationToken);

            await CreateUserNotification(notification.ReceiverUserId, newNotification.NotificationId, cancellationToken);

        }

        public async Task<CreateNotificationResponse> CreateNotification(string username, string subject, CancellationToken cancellationToken)
        {
            string content = string.Format(NOTIFICATION_CONTENT, username, subject);
            return await _mediator.Send(new CreateNotificationCommand
            {
                Content = content,
                Url = "#link"
            }, cancellationToken);
        }

        public async Task CreateUserNotification(int userId, int notificartionId, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new CreateUserNotificationPublisher
            {
                UserId = userId,
                NotificationId = notificartionId
            }, cancellationToken);
        }
    }
}

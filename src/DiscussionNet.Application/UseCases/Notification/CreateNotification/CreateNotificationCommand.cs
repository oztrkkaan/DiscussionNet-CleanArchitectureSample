using DiscussionNet.Application.Common.Interfaces;
using MediatR;

namespace DiscussionNet.Application.UseCases.Notification.CreateNotification
{
    internal record CreateNotificationCommand : IRequest<CreateNotificationResponse>
    {
        public string Content { get; init; }
        public string Url { get; init; }
    }
    internal class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationResponse>
    {
        IForumDbContext _context;

        public CreateNotificationCommandHandler(IForumDbContext context)
        {
            _context = context;
        }

        public async Task<CreateNotificationResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var newNotification = new Domain.Entities.Notification(request.Content, request.Url);

            await _context.Notifications.AddAsync(newNotification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateNotificationResponse
            {
                NotificationId = newNotification.Id
            };
        }
    }
    internal class CreateNotificationResponse
    {
        public int NotificationId { get; set; }
    }
}

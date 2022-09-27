using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.Email.RegistrationEmail.Publisher;
using DiscussionNet.Domain.Entities;
using MediatR;

namespace DiscussionNet.Application.Features.Email.EmailVerification
{
    public class CreateUserEmailVerificationEvent : INotification
    {
        public int UserId { get; set; }
    }

    public class CreateUserEmailVerificationEventHandler : INotificationHandler<CreateUserEmailVerificationEvent>
    {
        IForumDbContext _context;
        IMediator _mediator;
        public CreateUserEmailVerificationEventHandler(IForumDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task Handle(CreateUserEmailVerificationEvent request, CancellationToken cancellationToken)
        {
            var user = _context.Users.SingleOrDefault(m => m.Id == request.UserId);
            var emailVerification = new UserEmailVerification(user);

            await _context.UserEmailVerifications.AddAsync(emailVerification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            await _mediator.Publish(new SendRegistrationEmailPublisher
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Username = user.Username
            }, cancellationToken);
        }
    }
}

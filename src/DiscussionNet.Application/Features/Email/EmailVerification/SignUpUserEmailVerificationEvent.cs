using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.Email.RegistrationEmail.Publisher;
using DiscussionNet.Domain.Entities;
using MediatR;

namespace DiscussionNet.Application.Features.Email.EmailVerification
{
    public class SignUpUserEmailVerificationEvent : INotification
    {
        public int UserId { get; set; }
    }

    public class SignUpUserEmailVerificationEventHandler : INotificationHandler<SignUpUserEmailVerificationEvent>
    {
        IForumDbContext _context;
        IMediator _mediator;
        public SignUpUserEmailVerificationEventHandler(IForumDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task Handle(SignUpUserEmailVerificationEvent request, CancellationToken cancellationToken)
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

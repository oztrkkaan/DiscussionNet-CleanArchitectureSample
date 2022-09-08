using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Application.UseCases.Email.EmailVerification;
using Eskisehirspor.Application.UseCases.Email.RegistrationEmail.Publisher;
using MediatR;

namespace Eskisehirspor.Application.UseCases.User.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Username { get; init; }
        public string DisplayName { get; init; }
        public string Password { get; init; }
        public string PasswordConfirm { get; init; }
        public string Email { get; init; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IForumDbContext _context;
        private readonly IMediator _mediator;
        public CreateUserCommandHandler(IForumDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ThrowExceptionIfEmailExist(request.Email);
            ThrowExceptionIfUsernameExist(request.Username);

            Domain.Entities.User newUser = new(request.Username, request.DisplayName, request.Password, request.PasswordConfirm, request.Email);

            var newUserResult = await _context.Users.AddAsync(newUser, cancellationToken);
            ArgumentNullException.ThrowIfNull(newUserResult);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new CreateUserEmailVerificationEvent { UserId = newUser.Id }, cancellationToken);

            return new CreateUserResponse
            {
                IsSuccess = true
            };
        }

        private void ThrowExceptionIfUsernameExist(string username)
        {
            bool isUsernameExist = IsUsernameExist(username);
            if (isUsernameExist)
            {
                throw new Exception($"Username is already exist.");
            }
        }
        private void ThrowExceptionIfEmailExist(string email)
        {
            bool isEmailExist = IsEmailExist(email);
            if (isEmailExist)
            {
                throw new Exception($"Email is already exist.");
            }
        }
        private bool IsUsernameExist(string username)
        {
            return _context.Users.Any(m => m.Username == username);
        }
        private bool IsEmailExist(string email)
        {
            return _context.Users.Any(m => m.Email == email);
        }
    }

    public class CreateUserResponse
    {
        public bool IsSuccess { get; set; }
    }
}

using AutoMapper;
using DiscussionNet.Application.Common.Identity;
using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscussionNet.Application.UseCases.Authentication.SignIn
{
    public class SignInCommand : IRequest<SignInResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
    {
        IForumDbContext _context;
        ITokenService _tokenService;
        IMapper _mapper;
        public SignInCommandHandler(IForumDbContext context, ITokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await GetUserByUsernameOrEmail(request.UsernameOrEmail);
            VerifyUserPassword(request.Password, user);

            var authUser = _mapper.Map<AuthenticatedUser>(user);
            var token = _tokenService.CreateAccessToken(60 * 100000, authUser);

            return new SignInResponse
            {
                AccessToken = token.AccessToken,
                ExpirationDate = token.ExpirationDate,
                RefreshToken = token.RefreshToken
            };
        }

        private async Task<Domain.Entities.User> GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            var user = _context.Users.FirstOrDefaultAsync(m => m.Username == usernameOrEmail || m.Email == usernameOrEmail);
            if (user == null)
            {
                throw new Exception("Kullanıcı adı veya şifre yanlış.");
            }
            return await user;
        }

        private bool VerifyUserPassword(string requestPassword, Domain.Entities.User user)
        {
            var isVerifiedPassword = Domain.Entities.User.VerifyPasswordHash(requestPassword, user.PasswordHash, user.PasswordSalt);

            if (!isVerifiedPassword)
            {
                throw new Exception("Kullanıcı adı veya şifre yanlış.");
            }
            return isVerifiedPassword;
        }
    }

    public class SignInResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

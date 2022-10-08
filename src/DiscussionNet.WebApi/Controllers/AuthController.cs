using DiscussionNet.Application.Features.Authentication.SignIn;
using DiscussionNet.Application.Features.Authentication.SignUpUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : DefaultApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator) { }
        [HttpPost("sign-in")]
        public async Task<SignInResponse> SignIn(SignInCommand request, CancellationToken cancellationToken) => await _mediator.Send(request, cancellationToken);

        [Route("sign-up")]
        [HttpPost]
        public async Task<SignUpUserResponse> SignUp([FromBody] SignUpUserCommand request, CancellationToken cancellationToken) => await _mediator.Send(request, cancellationToken);
    }
}

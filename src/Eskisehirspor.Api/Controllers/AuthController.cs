using Eskisehirspor.Application.UseCases.Authentication.SignIn;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eskisehirspor.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : DefaultApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator) { }
        [HttpPost("sign-in")]
        public async Task<SignInResponse> SignIn(SignInCommand request, CancellationToken cancellationToken) => await _mediator.Send(request, cancellationToken);
    }
}

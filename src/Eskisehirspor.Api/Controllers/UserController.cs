using Eskisehirspor.Application.UseCases.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eskisehirspor.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : DefaultApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
        [Route("")]
        [HttpPost]
        public async Task<CreateUserResponse> Create([FromBody] CreateUserCommand request, CancellationToken cancellationToken) => await _mediator.Send(request, cancellationToken); 
    }
}

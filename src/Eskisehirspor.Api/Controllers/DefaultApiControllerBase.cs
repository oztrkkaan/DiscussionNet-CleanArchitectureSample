using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eskisehirspor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class DefaultApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected DefaultApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}

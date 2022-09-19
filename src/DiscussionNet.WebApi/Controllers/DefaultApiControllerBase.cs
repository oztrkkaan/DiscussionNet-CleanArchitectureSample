using DiscussionNet.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class DefaultApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IIdentityManager _identityManager;
        protected DefaultApiControllerBase(IMediator mediator, IIdentityManager? identityManager = null)
        {
            _mediator = mediator;
            if (identityManager != null)
            {
                _identityManager = identityManager;
            }
        }
    }
}

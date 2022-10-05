using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.Thread.CreateThread;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/thread")]
    [ApiController]
    public class ThreadController : DefaultApiControllerBase
    {
        public ThreadController(IMediator mediator, IIdentityManager? identityManager = null) : base(mediator, identityManager)
        {
        }
        [Route("")]
        [HttpPost]
        public async Task<CreateThreadResponse> Create(CreateThreadCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}

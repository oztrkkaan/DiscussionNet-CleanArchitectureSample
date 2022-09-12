using DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate;
using DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate.Publisher;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/thread-reaction")]
    [ApiController]
    public class ThreadReactionController : DefaultApiControllerBase
    {

        public ThreadReactionController(IMediator mediator) : base(mediator)
        {

        }
        [Route("")]
        [HttpPost]
        [Authorize]
        public async Task React([FromBody] CreateOrUpdateThreadReactionPublisher notification, CancellationToken cancellationToken) => await _mediator.Publish(notification, cancellationToken);
    }
}

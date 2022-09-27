using DiscussionNet.Application.Features.Topic.CreateTopic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/topic")]
    [ApiController]
    public class TopicController : DefaultApiControllerBase
    {
        public TopicController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Authorize]
        public async Task<CreateTopicResponse> Create(CreateTopicCommand request, CancellationToken cancellationToken) => await _mediator.Send(request, cancellationToken);
    }
}

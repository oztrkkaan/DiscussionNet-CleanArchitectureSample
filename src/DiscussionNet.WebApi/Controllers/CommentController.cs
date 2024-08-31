using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.Comment.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : DefaultApiControllerBase
    {
        public CommentController(IMediator mediator, IIdentityManager? identityManager = null) : base(mediator, identityManager)
        {
        }
        [Route("")]
        [HttpPost]
        public async Task<CreateCommentResponse> Create(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}

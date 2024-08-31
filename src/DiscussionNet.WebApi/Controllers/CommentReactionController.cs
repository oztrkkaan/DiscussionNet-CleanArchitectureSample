using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Features.CommentReactions.CreateOrUpdate.Publisher;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/comment-reaction")]
    [ApiController]
    public class CommentReactionController : DefaultApiControllerBase
    {

        public CommentReactionController(IMediator mediator, IIdentityManager identityManager) : base(mediator, identityManager) { }

        [Route("")]
        [HttpPost]
        [Authorize]
        public async Task React([FromBody] CreateOrUpdateCommentReactionPublisher notification, CancellationToken cancellationToken)
        {
            notification.ReactedUserId = _identityManager.User.Id;
            await _mediator.Publish(notification, cancellationToken);
        }
    }
}

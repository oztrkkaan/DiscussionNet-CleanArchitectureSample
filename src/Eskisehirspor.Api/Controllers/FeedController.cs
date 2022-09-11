using Eskisehirspor.Application.UseCases.Feed.GetLatestTopicsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Eskisehirspor.Application.UseCases.Feed.GetLatestTopicsQuery.GetLatestTopicsQueryHandler;

namespace Eskisehirspor.Api.Controllers
{
    [Route("api/feed")]
    [ApiController]
    public class FeedController : DefaultApiControllerBase
    {
        public FeedController(IMediator mediator) : base(mediator)
        {
        }
        [Route("latest-topics")]
        [HttpGet]
        public async Task<GetLatestTopicsQueryResponse> GetLatestTopics([FromQuery] GetLatestTopicsQuery request, CancellationToken cancellationToken) => await _mediator.Send(new GetLatestTopicsQuery { }, cancellationToken);
    }
}

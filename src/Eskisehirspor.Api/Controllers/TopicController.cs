﻿using Eskisehirspor.Application.UseCases.Topic.CreateTopic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskisehirspor.Api.Controllers
{
    [Route("api/[controller]")]
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

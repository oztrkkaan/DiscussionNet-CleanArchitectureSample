using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionNet.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : DefaultApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
  
    }
}

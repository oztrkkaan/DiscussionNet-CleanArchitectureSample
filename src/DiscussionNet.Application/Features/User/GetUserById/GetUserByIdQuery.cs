using DiscussionNet.Application.Common.Interfaces;
using MediatR;


namespace DiscussionNet.Application.UseCases.User.GetUserById
{
    public record GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public int UserId { get; init; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        IForumDbContext _context;

        public GetUserByIdQueryHandler(IForumDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == request.UserId);
           
            return new GetUserByIdResponse
            {
                Username = user.Username
            };
        }
    }

    public class GetUserByIdResponse
    {
        public string Username { get; set; }
    }
}

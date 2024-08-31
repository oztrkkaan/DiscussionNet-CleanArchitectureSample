using DiscussionNet.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DiscussionNet.Application.Features.Comment.CreateComment
{
    public class CreateCommentCommand : IRequest<CreateCommentResponse>
    {
        public string Content { get; set; }
        public int TopicId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentResponse>
    {
        IDiscussionDbContext _context;
        IIdentityManager _identityManager;
        IHttpContextAccessor _httpContextAccessor;
        private string _ipAddress;
        public CreateCommentCommandHandler(IDiscussionDbContext context, IIdentityManager identityManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _identityManager = identityManager;
            _httpContextAccessor = httpContextAccessor;
            _ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public async Task<CreateCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {

            var user = GetUserById(_identityManager.User.Id);
            var topic = GetTopicById(request.TopicId);
            var comment = new Domain.Entities.Comment(request.Content, topic, user, _ipAddress);

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateCommentResponse
            {
                Id = comment.Id
            };
        }

        private Domain.Entities.User GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == userId);
            ArgumentNullException.ThrowIfNull(user);

            return user;
        }
        private Domain.Entities.Topic GetTopicById(int topicId)
        {
            var topic = _context.Topics.FirstOrDefault(m => m.Id == topicId);
            ArgumentNullException.ThrowIfNull(topic);

            return topic;
        }
    }

    public class CreateCommentResponse
    {
        public int Id { get; set; }
    }
}

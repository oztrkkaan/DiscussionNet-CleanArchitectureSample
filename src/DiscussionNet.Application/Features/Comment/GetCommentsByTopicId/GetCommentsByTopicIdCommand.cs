using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscussionNet.Application.Features.Comment.GetCommentsByTopicId
{
    public class GetCommentsByTopicIdCommand : IRequest<GetCommentsByTopicIdResponse>
    {
        public int TopicId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 25;
        public CommentSortType SortType { get; set; } = CommentSortType.Today;
    }
    public class GetCommentsByTopicIdCommandHandler : IRequestHandler<GetCommentsByTopicIdCommand, GetCommentsByTopicIdResponse>
    {
        IDiscussionDbContext _context;
        public GetCommentsByTopicIdCommandHandler(IDiscussionDbContext context)
        {
            _context = context;
        }
        public async Task<GetCommentsByTopicIdResponse> Handle(GetCommentsByTopicIdCommand request, CancellationToken cancellationToken)
        {
            var comments = _context.Comments.Include(m => m.User).Where(m => m.Topic.Id == request.TopicId);

            if (request.SortType == CommentSortType.CreationDate)
            {
                comments = comments
                    .OrderBy(m => m.CreationDate);
            }
            else if (request.SortType == CommentSortType.MostLiked)
            {
                comments = comments
                    .Include(m => m.Reactions)
                    .OrderByDescending(m => m.Reactions.Count(x => x.Reaction == CommentReaction.Reactions.Like));
            }
            else if (request.SortType == CommentSortType.Today)
            {
                comments = comments
                    .Where(m => m.CreationDate.Date == DateTime.Now.Date)
                    .OrderByDescending(m => m.CreationDate);
            }

            var mappedComments = comments.Select(m => new GetCommentsByTopicIdResponse.Comment
            {
                Id = m.Id,
                Content = m.Content,
                CreationDate = m.CreationDate,
                ModifiedDate = m.ModifiedDate,
                User = new GetCommentsByTopicIdResponse.User
                {
                    DisplayName = m.User.DisplayName,
                    Username = m.User.Username
                }
            });

            var pagedComments = new PagedList<GetCommentsByTopicIdResponse.Comment>(mappedComments, request.PageNumber, request.PageSize);

            return new GetCommentsByTopicIdResponse
            {
                Comments = pagedComments
            };
        }
    }

    public class GetCommentsByTopicIdResponse
    {
        public IPagedList<Comment> Comments { get; set; }

        public class Comment
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public User User { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }
        public class User
        {
            public string Username { get; set; }
            public string DisplayName { get; set; }
        }
    }

    public enum CommentSortType
    {
        Today,
        CreationDate,
        MostLiked
    }
}

using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscussionNet.Application.Features.Thread.GetThreadsByTopicId
{
    public class GetThreadsByTopicIdCommand : IRequest<GetThreadsByTopicIdResponse>
    {
        public int TopicId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 25;
        public ThreadSortType SortType { get; set; } = ThreadSortType.Today;
    }
    public class GetThreadsByTopicIdCommandHandler : IRequestHandler<GetThreadsByTopicIdCommand, GetThreadsByTopicIdResponse>
    {
        IDiscussionDbContext _context;
        public GetThreadsByTopicIdCommandHandler(IDiscussionDbContext context)
        {
            _context = context;
        }
        public async Task<GetThreadsByTopicIdResponse> Handle(GetThreadsByTopicIdCommand request, CancellationToken cancellationToken)
        {
            var threads = _context.Threads.Include(m => m.User).Where(m => m.Topic.Id == request.TopicId);

            if (request.SortType == ThreadSortType.CreationDate)
            {
                threads = threads
                    .OrderBy(m => m.CreationDate);
            }
            else if (request.SortType == ThreadSortType.MostLiked)
            {
                threads = threads
                    .Include(m => m.Reactions)
                    .OrderByDescending(m => m.Reactions.Count(x => x.Reaction == ThreadReaction.Reactions.Like));
            }
            else if (request.SortType == ThreadSortType.Today)
            {
                threads = threads
                    .Where(m => m.CreationDate.Date == DateTime.Now.Date)
                    .OrderByDescending(m => m.CreationDate);
            }

            var mappedThreads = threads.Select(m => new GetThreadsByTopicIdResponse.Thread
            {
                Id = m.Id,
                Content = m.Content,
                CreationDate = m.CreationDate,
                ModifiedDate = m.ModifiedDate,
                User = new GetThreadsByTopicIdResponse.User
                {
                    DisplayName = m.User.DisplayName,
                    Username = m.User.Username
                }
            });

            var pagedThreads = new PagedList<GetThreadsByTopicIdResponse.Thread>(mappedThreads, request.PageNumber, request.PageSize);

            return new GetThreadsByTopicIdResponse
            {
                Threads = pagedThreads
            };
        }
    }

    public class GetThreadsByTopicIdResponse
    {
        public IPagedList<Thread> Threads { get; set; }

        public class Thread
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

    public enum ThreadSortType
    {
        Today,
        CreationDate,
        MostLiked
    }
}

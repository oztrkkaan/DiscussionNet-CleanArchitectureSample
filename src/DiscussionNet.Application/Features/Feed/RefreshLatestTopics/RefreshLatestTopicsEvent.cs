using DiscussionNet.Application.Common.Caching.Redis;
using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscussionNet.Application.Features.Feed.RefreshLatestTopics
{
    public class RefreshLatestTopicsEvent : INotification
    { }
    public class RefreshLatestTopicsEventHandler : INotificationHandler<RefreshLatestTopicsEvent>
    {
        private readonly IDiscussionDbContext _context;
        private readonly IRedisClient _redisClient;
        public RefreshLatestTopicsEventHandler(IDiscussionDbContext context, IRedisClient redisClient)
        {
            _context = context;
            _redisClient = redisClient;
        }
        public async Task Handle(RefreshLatestTopicsEvent notification, CancellationToken cancellationToken)
        {
            var feedItems = await _context.Topics
            .Include(m => m.Threads.Where(m => m.CreationDate >= DateTime.Now.AddDays(-15)))
            .OrderByDescending(m => m.CreationDate)
            .Take(50)
            .Select(m => new FeedItem
            {
                Subject = m.Subject,
                ThreadCount = m.ThreadCount,
                LastThreadCreationDate = m.Threads.OrderByDescending(x => x.CreationDate).FirstOrDefault().CreationDate
            }).ToListAsync();

            await _redisClient.SetAsync("feed:latest-topics", feedItems, new TimeSpan(1, 0, 0));
        }
    }
}

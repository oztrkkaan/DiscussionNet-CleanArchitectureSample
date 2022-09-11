using Eskisehirspor.Application.Common.Caching.Redis;
using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eskisehirspor.Application.UseCases.Feed.RefreshLatestTopics
{
    public class RefreshLatestTopicsEvent : INotification
    { }
    public class RefreshLatestTopicsEventHandler : INotificationHandler<RefreshLatestTopicsEvent>
    {
        private readonly IForumDbContext _context;
        private readonly IRedisClient _redisClient;
        public RefreshLatestTopicsEventHandler(IForumDbContext context, IRedisClient redisClient)
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

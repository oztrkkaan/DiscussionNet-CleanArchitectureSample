using Eskisehirspor.Application.Common.Interfaces;
using MediatR;
using Eskisehirspor.Application.Common.Caching.Redis;

namespace Eskisehirspor.Application.UseCases.Feed.LatestThreads
{
    public class GetLatestTopicsEvent : INotification
    { }

    public class GetLatestTopicsJobHandler : INotificationHandler<GetLatestTopicsEvent>
    {
        IForumDbContext _context;
        IRedisClient _redisClient;

        public GetLatestTopicsJobHandler(IForumDbContext context, IRedisClient redisClient)
        {
            _context = context;
            _redisClient = redisClient;
        }

        public async Task Handle(GetLatestTopicsEvent notification, CancellationToken cancellationToken)
        {
            var topics = _context.Threads.Where(m => m.CreationDate >= DateTime.Now.AddDays(-15)).OrderByDescending(m => m.CreationDate).ToList();
            //.GroupBy(m => m.Topic.Id)
            //.Select(m => new FeedItem
            //{
            //    Subject = m.Key.ToString(),
            //    ThreadCount = m.Count()
            //}).ToList();
            topics.ForEach(m => Console.WriteLine(m.Content));
            _redisClient.GetFromCacheOrCreate<string>("IsRegistered", 1, () => { return "1"; });



        }
    }
}

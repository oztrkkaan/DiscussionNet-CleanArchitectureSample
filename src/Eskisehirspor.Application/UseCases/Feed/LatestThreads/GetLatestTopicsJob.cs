using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Domain.Entities;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Feed.LatestThreads
{
    public class GetLatestTopicsJob : INotification
    {
        public string x { get; set; }
    }

    public class GetLatestTopicsJobHandler : INotificationHandler<GetLatestTopicsJob>
    {
        IForumDbContext _context;

        public GetLatestTopicsJobHandler(IForumDbContext context)
        {
            _context = context;
        }

        public async Task Handle(GetLatestTopicsJob notification, CancellationToken cancellationToken)
        {
            var topics = _context.Threads.Where(m => m.CreationDate >= DateTime.Now.AddDays(-1)).OrderByDescending(m => m.CreationDate).TakeLast(1000).GroupBy(m => new FeedItem
            {
                Subject = m.Topic.Subject,
                ThreadCount = m.Topic.ThreadCount,
                Url = m.Topic.UrlName
            }).ToList();
        }
    }
}

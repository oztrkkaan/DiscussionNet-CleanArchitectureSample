using MediatR;
using static DiscussionNet.Application.Features.Feed.GetLatestTopicsQuery.GetLatestTopicsQueryHandler;
using DiscussionNet.Domain.Entities;
using DiscussionNet.Application.Common.Caching.Redis;

namespace DiscussionNet.Application.Features.Feed.GetLatestTopicsQuery
{
    public class GetLatestTopicsQuery : IRequest<GetLatestTopicsQueryResponse>
    { }

    public class GetLatestTopicsQueryHandler : IRequestHandler<GetLatestTopicsQuery, GetLatestTopicsQueryResponse>
    {
        IRedisClient _redisClient;

        public GetLatestTopicsQueryHandler(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task<GetLatestTopicsQueryResponse> Handle(GetLatestTopicsQuery query, CancellationToken cancellationToken)
        {
            return new GetLatestTopicsQueryResponse
            {
                FeedItems = await _redisClient.GetAsync<List<FeedItem>>("feed:latest-topics")
            };
        }

        public class GetLatestTopicsQueryResponse
        {
            public List<FeedItem> FeedItems { get; set; }
        }
    }
}
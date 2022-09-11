using MediatR;
using Eskisehirspor.Application.Common.Caching.Redis;
using static Eskisehirspor.Application.UseCases.Feed.GetLatestTopicsQuery.GetLatestTopicsQueryHandler;
using Eskisehirspor.Domain.Entities;

namespace Eskisehirspor.Application.UseCases.Feed.GetLatestTopicsQuery
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
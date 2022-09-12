namespace DiscussionNet.Application.Common.Caching.Redis
{
    public interface IRedisClientConfigProvider
    {
        RedisConfig GetRedisConfig();
    }
}

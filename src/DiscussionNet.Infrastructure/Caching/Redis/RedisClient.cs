using DiscussionNet.Application.Common.Caching.Redis;
using DiscussionNet.Application.Common.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
namespace DiscussionNet.Infrastructure.Caching.Redis
{
    public class RedisClient : IRedisClient
    {
        private static readonly object _cacheLockObject = new object();
        private readonly IDistributedCache _redisCache;
        private TimeSpan DefaultExpirationTimeSpan { get; set; }

        public RedisClient(IRedisClientProvider redisClientProvider)
        {
            DefaultExpirationTimeSpan = new TimeSpan(0, 0, 5, 0);
            _redisCache = redisClientProvider.GetDistributedCache();
        }

        public T Get<T>(string key)
        {
            if (_redisCache.GetString(key) != null)
            {
                return _redisCache.GetString(key).DeserializeJSON<T>();
            }
            else
            {
                return default;
            }

        }
        public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            var response = await _redisCache.GetStringAsync(key, token);
            T item = default;
            if (response != null)
                item = response.DeserializeJSON<T>();
            return await Task.FromResult(item);
        }

        public void Refresh(string key)
        {
            _redisCache.Refresh(key);
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            return _redisCache.RefreshAsync(key, token);
        }

        public void Remove(string key)
        {
            _redisCache.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            return _redisCache.RemoveAsync(key, token);
        }

        public void Set(string key, string value, DistributedCacheEntryOptions options)
        {
            _redisCache.SetString(key, value, options);
        }


        public void Set(string key, string value)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = DefaultExpirationTimeSpan
            };
            _redisCache.SetString(key, value, options);
        }

        public Task SetAsync(string key, string value, CancellationToken token = default)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = DefaultExpirationTimeSpan
            };
            return _redisCache.SetStringAsync(key, value, options, token);
        }
        public Task SetAsync(string key, string value, TimeSpan expirationTimeSpan, CancellationToken token = default)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expirationTimeSpan
            };
            return _redisCache.SetStringAsync(key, value, options, token);
        }
        public string Get(string key)
        {
            byte[] redisData = _redisCache.Get(key);
            string result = string.Empty;

            if (redisData != null && redisData.Length > 0)
            {
                result = Encoding.Default.GetString(redisData);
            }

            return result;
        }

        public T GetFromCacheOrCreate<T>(string key, int minutes, Func<T> createOperation) where T : class
        {
            T result = Get<T>(key);

            if (result != null)
            {
                return result;
            }

            lock (_cacheLockObject)
            {
                if (result != null)
                {
                    return result;
                }

                T operationResult = createOperation();
                if (operationResult != null)
                {
                    Set(key, operationResult, new TimeSpan(0, 0, minutes, 0));
                    return operationResult;
                }
            }
            return null;
        }

        public void Set<T>(string key, T value, TimeSpan expirationTimeSpan)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expirationTimeSpan
            };
            var data = value.ToJSON();
            _redisCache.SetString(key, data, options);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan expirationTimeSpan)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expirationTimeSpan
            };
            var data = value.ToJSON();
            return _redisCache.SetStringAsync(key, data, options);
        }


        public void Set(string key, string value, TimeSpan expirationTime)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };

            _redisCache.Set(key, Encoding.Default.GetBytes(value), options);
        }
    }
}

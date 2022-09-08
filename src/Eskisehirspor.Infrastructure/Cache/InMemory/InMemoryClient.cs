using Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eskisehirspor.Infrastructure.Cache.InMemory
{
    public class InMemoryClient : IInMemoryClient
    {
        private static readonly object _cacheLockObject = new object();
        private IMemoryCache inMemoryCache { get; set; }
        private TimeSpan DefaultExpirationTimeSpan { get; set; }

        public InMemoryClient(IMemoryCache inMemoryCache)
        {

            DefaultExpirationTimeSpan = new TimeSpan(0, 0, 5, 0);
            this.inMemoryCache = inMemoryCache;
        }

        public T Get<T>(string key)
        {
            object cacheValue = inMemoryCache.Get(key);
            if (cacheValue == null) return default;
            return (T)cacheValue;
        }

        public string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;
            object cacheValue = inMemoryCache.Get(key);
            if (cacheValue == null) return null;
            return cacheValue.ToString();
        }

        public Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public T GetFromCacheOrCreate<T>(string cacheKey, int minutes, Func<T> createOperation) where T : class
        {
            T operationResult = createOperation();
            if (operationResult != null)
                return inMemoryCache.GetOrCreate(cacheKey, cacheEntry => operationResult);
            return null;
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            inMemoryCache.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, TimeSpan expirationTimeSpan)
        {
            inMemoryCache.Set(key, value);
        }

        public void Set(string key, string value)
        {
            inMemoryCache.Set(key, value, DefaultExpirationTimeSpan);
        }

        public void Set(string key, string value, TimeSpan expirationTimeSpan)
        {
            inMemoryCache.Set(key, value, DateTimeOffset.Now.Add(expirationTimeSpan));
        }

        public Task SetAsync<T>(string key, T value, TimeSpan expirationTimeSpan)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, string value, TimeSpan expirationTimeSpan, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, string value, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, string value, MemoryCacheEntryOptions options)
        {
            inMemoryCache.Set(key, value, options);
        }

        public void Set<T>(string key, T value, MemoryCacheEntryOptions options)
        {
            inMemoryCache.Set(key, value, options);
        }


    }
}

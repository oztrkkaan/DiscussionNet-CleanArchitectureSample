using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces
{
    public interface IInMemoryClient
    {
        T Get<T>(string key);
        string Get(string key);
        Task<T> GetAsync<T>(string key, CancellationToken token = default);
        void Refresh(string key);
        Task RefreshAsync(string key, CancellationToken token = default);
        void Remove(string key);
        Task RemoveAsync(string key, CancellationToken token = default);
        void Set<T>(string key, T value, TimeSpan expirationTimeSpan);
        Task SetAsync<T>(string key, T value, TimeSpan expirationTimeSpan);
        void Set(string key, string value);
        void Set(string key, string value, TimeSpan expirationTime);
        Task SetAsync(string key, string value, TimeSpan expirationTimeSpan, CancellationToken token = default);
        Task SetAsync(string key, string value, CancellationToken token = default);
        T GetFromCacheOrCreate<T>(string cacheKey, int minutes, Func<T> createOperation) where T : class;
        void Set(string key, string value, MemoryCacheEntryOptions options);

        void Set<T>(string key, T value, MemoryCacheEntryOptions options);
    }
}

using Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.InMemory
{
    public class InMemoryClientProvider : IInMemoryClientProvider
    {
        private IMemoryCache _memoryCache;
        public InMemoryClientProvider()
        {
            _memoryCache = GetMemoryCache();
        }

        public void Dispose()
        {
            //_memoryCache?.Dispose();
        }

        public IInMemoryClient GetInMemoryClient()
        {
            return new InMemoryClient(_memoryCache);
        }

        public IMemoryCache GetMemoryCache()
        {
            IOptions<MemoryCacheOptions> options = new MemoryCacheOptions()
            {

            };
            return new MemoryCache(options);
        }
    }
}

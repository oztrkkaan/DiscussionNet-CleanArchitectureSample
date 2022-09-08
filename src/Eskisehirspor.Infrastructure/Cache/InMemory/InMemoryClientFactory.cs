using Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.InMemory
{
    public class InMemoryClientFactory : IInMemoryClientFactory
    {
        private IInMemoryClientProvider inMemoryClientProvider { get; set; }

        public InMemoryClientFactory(IInMemoryClientProviderFactory inMemoryClientProviderFactory)
        {
            inMemoryClientProvider = inMemoryClientProviderFactory.GetInMemoryClientProvider();
        }

        public IInMemoryClient GetInMemoryClient()
        {
            return inMemoryClientProvider.GetInMemoryClient();
        }
    }
}

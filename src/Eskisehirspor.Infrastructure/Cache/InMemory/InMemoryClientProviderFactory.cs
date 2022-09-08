using Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.InMemory
{
    class InMemoryClientProviderFactory : IInMemoryClientProviderFactory
    {
        public InMemoryClientProviderFactory()
        {
        }

        public IInMemoryClientProvider GetInMemoryClientProvider()
        {
            return new InMemoryClientProvider();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.InMemory.Interfaces
{
    public interface IInMemoryClientProvider
    {
        IInMemoryClient GetInMemoryClient();
    }
}

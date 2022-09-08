using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.Redis.Interfaces
{
    public interface IRedisClientProviderFactory
    {
        IRedisClientProvider GetRedisClientProvider();
    }
}

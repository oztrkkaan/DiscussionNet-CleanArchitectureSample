﻿using Eskisehirspor.Infrastructure.Cache.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eskisehirspor.Infrastructure.Cache.Redis.Interfaces
{
    public interface IRedisClientConfigProvider
    {
        RedisConfig GetRedisConfig(string redisName);
    }
}

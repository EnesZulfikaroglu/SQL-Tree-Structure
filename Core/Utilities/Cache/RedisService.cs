using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace Core.Utilities.Cache
{
    public class RedisService
    {
        ConnectionMultiplexer connectionMultiplexer;
        public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");
        public IDatabase GetDb(int db) => connectionMultiplexer.GetDatabase(db);
    }
}

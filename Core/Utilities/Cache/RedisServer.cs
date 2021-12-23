using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Core.Utilities.Cache
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationString = "localhost:6379";
        private int _currentDatabaseId = 0;
        public RedisServer()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationString + ",allowAdmin=true");
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }
        public IDatabase Database => _database;
        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(configurationString).FlushDatabase(_currentDatabaseId);
        }

        public List<RedisKey> GetKeys()
        {
            return _connectionMultiplexer.GetServer(configurationString).Keys().ToList();
        }
    }
}
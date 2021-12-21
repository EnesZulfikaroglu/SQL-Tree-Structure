using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Cache
{
    public class RedisConnectionFactory
    {
        private static string ConnectionString = "localhost:6379";
        static RedisConnectionFactory()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConnectionString); // Redis server connection string
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public static void DisposeConnection()
        {
            if (lazyConnection.Value.IsConnected)
                lazyConnection.Value.Dispose();
            Connection.GetServer("").FlushDatabase();
        }
    }
}

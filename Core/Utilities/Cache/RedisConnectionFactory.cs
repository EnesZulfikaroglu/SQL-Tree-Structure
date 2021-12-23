using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Cache
{
    public class RedisConnectionFactory
    {
        private static string ConnectionString = "localhost:6379";
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
            return ConnectionMultiplexer.Connect(ConnectionString + ",abortConnect=false,ssl=true");
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public static void DisposeConnection()
        {
            if (lazyConnection.Value.IsConnected)
                lazyConnection.Value.Dispose();
            Connection.GetServer("").FlushDatabase();
        }
    }
}

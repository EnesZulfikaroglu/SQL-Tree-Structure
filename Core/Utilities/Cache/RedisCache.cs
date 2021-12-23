using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core.Utilities.Cache
{
    public class RedisCache : ICache
    {
        private RedisServer _redisServer;

        // Setting the connection while initializing
        public RedisCache(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        // Returns true if connected successfully
        public bool CheckConnectionWithTimeLimit(TimeSpan timeSpan)
        {
            try
            {
                Task task = Task.Factory.StartNew(() => CheckConnection());
                task.Wait(timeSpan);
                return task.IsCompleted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Returns true if connected successfully
        public void CheckConnection()
        {
            try
            {
                _redisServer.Database.Ping();
            }
            catch (Exception)
            {
            }
        }

        // Redis - Setting Data
        public void Set<T>(string key, T objectToCache, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);

            _redisServer.Database.StringSet(key, JsonConvert.SerializeObject(objectToCache), expireTimeSpan);
        }

        // Redis - Getting data
        public T Get<T>(string key)
        {
            var redisObject = _redisServer.Database.StringGet(key);

            return redisObject.HasValue ? JsonConvert.DeserializeObject<T>(redisObject) : Activator.CreateInstance<T>();
        }

        // Redis - Deleting a key
        public void Delete(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void FlushAll()
        {
            _redisServer.FlushDatabase();

            Console.WriteLine("Redis Database flushed");
        }

        // Returns true if key exists
        public bool Exists(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        // Redis - Disposing Connection

    }
}

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
        private readonly IDatabase _redisDb;

        // Setting the connection while initializing
        public RedisCache()
        {
            _redisDb = RedisConnectionFactory.Connection.GetDatabase();
        }

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
                _redisDb.Ping();
            }
            catch (Exception)
            {
            }
        }

        //Redis'e json formatında set işlemi yapılan metot
        public void Set<T>(string key, T objectToCache, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);

            _redisDb.StringSet(key, JsonConvert.SerializeObject(objectToCache), expireTimeSpan);
        }

        //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
        public T Get<T>(string key)
        {
            var redisObject = _redisDb.StringGet(key);

            return redisObject.HasValue ? JsonConvert.DeserializeObject<T>(redisObject) : Activator.CreateInstance<T>();
        }

        //Redis te var olan key-value değerlerini silen metot
        public void Delete(string key)
        {
            _redisDb.KeyDelete(key);
        }

        // Returns true if key exists
        public bool Exists(string key)
        {
            return _redisDb.KeyExists(key);
        }

        //Redis bağlantısını Dispose eden metot
        public void Dispose()
        {
            RedisConnectionFactory.Connection.Dispose();
        }
    }
}

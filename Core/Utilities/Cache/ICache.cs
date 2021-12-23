using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Cache
{
    public interface ICache : IDisposable
    {
        bool CheckConnectionWithTimeLimit(TimeSpan timeSpan);

        void CheckConnection();
        T Get<T>(string key);

        void Set<T>(string key, T obj, DateTime expireDate);

        void Delete(string key);

        void FlushAll();

        bool Exists(string key);
    }
}

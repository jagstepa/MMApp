using System;
using System.Runtime.Caching;

namespace MMApp.Web.Helpers
{
    public class InMemoryCache : ICache
    {
        private static readonly object CacheLockObject = new object();

        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                lock (CacheLockObject)
                {
                    item = getItemCallback();
                    MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
                }
            }
            return item;
        }

        public void RemoveItem(string cacheKey)
        {
            object obj = MemoryCache.Default.Get(cacheKey);
            if (obj != null)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }

        public void ClearAll()
        {
            MemoryCache.Default.Dispose();
        }

        public string Get(string cacheKey)
        {
            var key = MemoryCache.Default.Get(cacheKey) as string;
            return key;
        }

        public void Set(string cacheKey, string cacheValue)
        {
            MemoryCache.Default.Add(cacheKey, cacheValue, DateTime.Now.AddMinutes(10));
        }
    }
}
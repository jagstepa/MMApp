using System;

namespace MMApp.Web.Helpers
{
    public interface ICache
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        void RemoveItem(string cacheKey);
    }
}
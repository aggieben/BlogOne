using System;

namespace BlogOne.Common.Cache
{
    using Cache = System.Web.Caching.Cache;

    public class LocalCache : ICache
    {
        private readonly Cache _cache;

        public LocalCache(Cache cache)
        {
            _cache = cache;
        }        

        public T Get<T>(string key)
        {
            return (T) _cache[key];
        }

        public void Set<T>(string key, T value)
        {
            _cache[key] = value;
        }

        /// <summary>
        /// Returns the old value, asynchronously sets it using the given <c>Func</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="inout"></param>
        /// <returns>old value</returns>
        public T GetSet<T>(string key, Func<T, T> inout)
        {
            var oldVal = (T)_cache[key];
            inout.Invoke(oldVal);
            return oldVal;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}

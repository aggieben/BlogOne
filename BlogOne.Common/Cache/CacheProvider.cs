using StackExchange.Redis;
using System;
using System.Linq;
using System.Web.Configuration;

namespace BlogOne.Common.Cache
{
    public class CacheProvider
    {
        private ConnectionMultiplexer _redisConnectionMultiplexer;
        private readonly System.Web.Caching.Cache _cache;

        public CacheProvider(System.Web.Caching.Cache webCache)
        {
            _cache = webCache;
        }

        public ICache GetCache(string redisConfigName)
        {
            ICache cacheResult = new LocalCache(_cache);
            if (!WebConfigurationManager.AppSettings.AllKeys.Contains(redisConfigName)) 
                return cacheResult;

            // this should be a redis connection string
            var redisConfig = WebConfigurationManager.AppSettings[redisConfigName];

            // try connecting to redisConfig; if success, then return RedisCache.  Else, return LocalCache
            try
            {
                _redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConfig);
                var db = _redisConnectionMultiplexer.GetDatabase();
                cacheResult = new RedisCache(db);
            }
            catch (Exception ex)
            {
                // TODO: logging                    
            }

            return cacheResult;
        }
    }
}

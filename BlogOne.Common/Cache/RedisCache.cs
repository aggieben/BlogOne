using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using StackExchange.Redis;

namespace BlogOne.Common.Cache
{
    public class RedisCache : ICache
    {
        private IDatabase _db;

        public RedisCache(IDatabase db)
        {
            _db = db;
        }

        public T Get<T>(string key)
        {
            var val = default(T);
            var redisVal = _db.StringGet(key);

            if (redisVal.IsNullOrEmpty) return val;

            using (var ms = new MemoryStream(redisVal))
            {
                val = Serializer.Deserialize<T>(ms);
            }

            return val;
        }

        public void Set<T>(string key, T value)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, value);
                _db.StringSet(key, ms.GetBuffer());
            }
        }

        public T GetSet<T>(string key, Func<T, T> inout)
        {
            var oldVal = default(T);
            var redisVal = _db.StringGet(key);
            
            if (redisVal.IsNullOrEmpty) return oldVal;
            
            using (var ms = new MemoryStream(redisVal))
            {
                oldVal = Serializer.Deserialize<T>(ms);
            }

            var newVal = inout(oldVal);
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, newVal);
                _db.StringSet(key, ms.GetBuffer());
            }

            return oldVal;
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}

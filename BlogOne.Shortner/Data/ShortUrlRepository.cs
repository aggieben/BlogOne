using System.Transactions;
using System.Web.Caching;
using System.Web.Mvc;
using BlogOne.Common.Cache;
using BlogOne.Common.Data;
using BlogOne.Common.Extensions;
using BlogOne.Shortner.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BlogOne.Shortner.Data
{
    public class ShortUrlRepository : Repository<ShortUrl>, IShortUrlRepository
    {
        private readonly ICache _cache;

        public ShortUrlRepository(IDbConnectionFactory factory, ICache cache) : base(factory)
        {
            _cache = cache;
        }

        protected override void AddImpl(ShortUrl item)
        {
           // using (var scope = new TransactionScope())
            using (var db = Connection)
            {
                var id = db.Insert(item);
                var su = FindById(id);
                su.ShortCode = id.ToBase62();
                Update(su);
            }
        }

        protected override void RemoveImpl(ShortUrl item)
        {
            const string sql = @"
delete 
  from ShortUrls
 where item.Id = {=Id}
";

            using (var db = Connection)
            {
                if (item.Id.HasValue)
                {
                    db.Delete(item);
                }
                else
                {
                    if (0 <= db.Execute(sql, new {item.Id}))
                    {
                        throw new DataException("Failed to remove entity");
                    }
                }
            }
        }

        protected override void UpdateImpl(ShortUrl item)
        {
            const string sql = @"
  select *
    from ShortUrls
   where Id = {=Id}
";

            using (var db = Connection)
            {
                if (item.Id.HasValue)
                {
                    if (!db.Update(item))
                    {
                        throw new DataException("Failed to update entity");
                    }
                }
                else
                {
                    var entity = db.Query<ShortUrl>(sql, new {item.Id }).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Enabled = item.Enabled;
                        entity.ShortCode = item.ShortCode;
                        entity.Url = item.Url;
                        this.Update(entity);
                    }
                }
            }
        }

        protected override ShortUrl FindBySidImpl(Guid id)
        {
            const string sql = @"
  select *
    from ShortUrls
   where Sid = {=Sid}
";
            using (var db = Connection)
            {
                return db.Query<ShortUrl>(sql, new {Sid = id}).FirstOrDefault();
            }
        }

        protected override ShortUrl FindByIdImpl(int id)
        {
            using (var db = Connection)
            {
                return db.Get<ShortUrl>(id);
            }
        }

        protected override IEnumerable<ShortUrl> FindAllImpl(int page, int pageSize)
        {
            const string sql = @"
  select *
    from ShortUrls
order by CreationDate desc
offset {=skip} rows fetch next {=size} rows only
";
            using (var db = Connection)
            {
                return db.Query<ShortUrl>(sql, new {skip = page*pageSize, size = pageSize});
            }
        }

        public ShortUrl FindByShortCode(string shortCode)
        {
            var sql = @"
  select * 
    from ShortUrls
   where Id = {=Id}
";

            var id = shortCode.FromBase62();

            var result = _cache.Get<ShortUrl>(String.Format("ShortUrl/{0}", id));
            if (result == null)
            {
                using (var db = Connection)
                {
                    result = db.Query<ShortUrl>(sql, new {Id = id}).FirstOrDefault();
                }
            }

            return result;
        }
    }
}
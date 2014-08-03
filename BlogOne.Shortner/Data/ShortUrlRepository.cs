using BlogOne.Common.Data;
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
        public ShortUrlRepository(IDbConnectionFactory factory) : base(factory) { }

        protected override void AddImpl(ShortUrl item)
        {
            using (var db = Connection)
            {
                db.Insert(item);
            }
        }

        protected override void RemoveImpl(ShortUrl item)
        {
            using (var db = Connection)
            {
                if (item.Id.HasValue)
                {
                    db.Delete(item);
                }
                else
                {
                    const string sql = @"
delete 
  from ShortUrls
 where item.Id = {=Id}
";
                    if (0 <= db.Execute(sql, new {item.Id}))
                    {
                        throw new DataException("Failed to remove entity");
                    }
                }
            }
        }

        protected override void UpdateImpl(ShortUrl item)
        {
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
                    const string sql = @"
  select *
    from ShortUrls
   where Id = {=Id}
";
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
            throw new NotImplementedException();
        }

        protected override ShortUrl FindByIdImpl(int id)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<ShortUrl> FindAllImpl(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ShortUrl FindByShortCode(string shortCode)
        {
            throw new NotImplementedException();
        }
    }
}
using BlogOne.Common.Data;
using BlogOne.Web.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BlogOne.Web.Data
{
    public class ExternalLoginRepository : Repository<ExternalLogin>, IExternalLoginRepository
    {
        public ExternalLoginRepository(IDbConnectionFactory factory) : base(factory) { }

        protected override void AddImpl(ExternalLogin item)
        {
            using (var db = Connection)
            {
               db.Insert(item);
            }
        }

        protected override void RemoveImpl(ExternalLogin item)
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
delete from ExternalLogins el 
      where el.LoginProvider = @loginProvider
        and el.ProviderKey = @providerKey";

                    if (0 <= db.Execute(sql, new { loginProvider = item.LoginProvider, providerKey = item.ProviderKey }))
                    {
                        throw new DataException("Failed to remove entity");
                    }
                }
            }
        }

        protected override void UpdateImpl(ExternalLogin item)
        {
            using (var db = Connection)
            {
                if (item.Id.HasValue)
                {
                    if (!db.Update(item))
                    {
                        throw new DataException("Failed to update ExternalLogin");
                    }
                }
                else
                {
                    const string querySql = @"
select *
  from ExternalLogins el
 where el.LoginProvider = @loginProvider
   and el.ProviderKey = @providerKey";

                    var entity = db.Query(querySql, new { loginProvider = item.LoginProvider, providerKey = item.ProviderKey }).FirstOrDefault();
                    if (entity.HasValue())
                    {
                        this.Update(entity);
                    }
                }
            }
        }

        protected override ExternalLogin FindBySidImpl(Guid id)
        {
            const string sql = @"
select *
  from ExternalLogins el
 where el.Sid = @guid";

            using (var db = Connection)
            {
                return db.Query(sql, new { guid = id }).FirstOrDefault();
            }
        }

        protected override ExternalLogin FindByIdImpl(int id)
        {
            using (var db = Connection)
            {
                return db.Get<ExternalLogin>(id);
            }
        }

        public ExternalLogin FindByProviderAndKey(string provider, string key)
        {
            const string sql = @"
select *
  from ExternalLogins el
 where el.LoginProvider = @provider
   and el.ProviderKey = @key;";

            using (MiniProfiler.Current.Step("Repository.FindByProviderAndKey"))
            using (var db = Connection)
            {
                return db.Query<ExternalLogin>(sql, new { provider, key }).FirstOrDefault();
            }
        }

        protected override IEnumerable<ExternalLogin> FindAllImpl(int page, int pageSize)
        {
            // punting on paging these for now, cuz there should only ever be a few of them
            const string sql = @"
select *
  from ExternalLogins";

            using (var db = Connection)
            {
                return db.Query<ExternalLogin>(sql);
            }
        }
    }
}
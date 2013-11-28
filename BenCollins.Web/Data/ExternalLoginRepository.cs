using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Contrib.Extensions;
using BenCollins.Web.Model;
using System.Data;
using BenCollins.Web.Extensions;

namespace BenCollins.Web.Data
{
    public class ExternalLoginRepository : Repository<ExternalLogin>, IExternalLoginRepository
    {
        public ExternalLoginRepository(IDbConnectionFactory factory) : base(factory) { }

        public override void Add(ExternalLogin item)
        {
            using (var db = Connection)
            {
                db.Insert(item);
            }
        }

        public override void Remove(ExternalLogin item)
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

        public override void Update(ExternalLogin item)
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

        public override ExternalLogin FindBySid(Guid id)
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

        public override ExternalLogin FindById(int id)
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

            using (var db = Connection)
            {
                return db.Query<ExternalLogin>(sql, new { provider, key }).FirstOrDefault();
            }
        }

        public override IEnumerable<ExternalLogin> FindAll()
        {
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
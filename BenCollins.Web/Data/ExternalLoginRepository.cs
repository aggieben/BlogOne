using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BenCollins.Web.Data
{
    public class ExternalLoginRepository : Repository<ExternalLoginInfo>, IExternalLoginRepository
    {
        public ExternalLoginRepository(IDbConnectionFactory factory) : base(factory) { }

        public override void Add(ExternalLoginInfo item)
        {
            using (var db = Connection)
            {
                db.Insert(item);
            }
        }

        public override void Remove(ExternalLoginInfo item)
        {
            throw new NotImplementedException();
        }

        public override void Update(ExternalLoginInfo item)
        {
            throw new NotImplementedException();
        }

        public override ExternalLoginInfo FindBySid(Guid id)
        {
            throw new NotImplementedException();
        }

        public override ExternalLoginInfo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ExternalLoginInfo> Find(System.Linq.Expressions.Expression<Func<ExternalLoginInfo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ExternalLoginInfo> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
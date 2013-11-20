using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenCollins.Web.Data
{
    public class ExternalLoginRepository : IRepository<ExternalLoginInfo>
    {
        public void Add(ExternalLoginInfo item)
        {
            throw new NotImplementedException();
        }

        public void Remove(ExternalLoginInfo item)
        {
            throw new NotImplementedException();
        }

        public void Update(ExternalLoginInfo item)
        {
            throw new NotImplementedException();
        }

        public ExternalLoginInfo FindBySid(Guid id)
        {
            throw new NotImplementedException();
        }

        public ExternalLoginInfo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExternalLoginInfo> Find(System.Linq.Expressions.Expression<Func<ExternalLoginInfo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExternalLoginInfo> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
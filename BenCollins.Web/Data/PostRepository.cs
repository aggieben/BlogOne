using BenCollins.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BenCollins.Web.Data
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(IDbConnectionFactory factory) : base(factory) { }

        protected override void AddImpl(Post item)
        {
            using (var db = Connection)
            {
                db.Insert(item);
            }
        }

        protected override void RemoveImpl(Post item)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateImpl(Post item)
        {
            throw new NotImplementedException();
        }

        protected override Post FindBySidImpl(Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Post FindByIdImpl(int id)
        {
            using (var db = Connection)
            {
                return db.Get<Post>(id);
            }
        }

        protected override IEnumerable<Post> FindAllImpl()
        {
            throw new NotImplementedException();
        }
    }
}
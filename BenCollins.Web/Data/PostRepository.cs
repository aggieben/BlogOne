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

        public Post FindBySlug(string slug)
        {
            using (var db = Connection)
            {
                const string sql = @"
select *
  from Posts p
 where p.SlugHash = checksum(@slug)";

                return db.Query<Post>(sql, new { slug }).SingleOrDefault();
            }
        }

        protected override void AddImpl(Post item)
        {
            using (var db = Connection)
            {
                db.Insert(item);
            }
        }

        protected override void RemoveImpl(Post item)
        {
            using (var db = Connection)
            {
                db.Delete(item);
            }
        }

        protected override void UpdateImpl(Post item)
        {
            using (var db = Connection)
            {
                db.Update(item);
            }
        }

        protected override Post FindBySidImpl(Guid id)
        {
            const string sql = @"
select *
  from Posts
 where Sid = @id";

            using (var db = Connection)
            {
                return db.Query(sql, new { id }).FirstOrDefault();
            }
        }

        protected override Post FindByIdImpl(int id)
        {
            using (var db = Connection)
            {
                return db.Get<Post>(id);
            }
        }

        protected override IEnumerable<Post> FindAllImpl(int page, int pageSize)
        {
            const string sql = @"
with NumberedPosts as
(
    select row_number() over (order by Id) as Row, Id
      from Posts
)
select *
  from NumberedPosts np
       inner join Posts p on np.Id = p.Id
 where np.Row between @pageBegin and @pageEnd
order by np.Row desc";

            using (var db = Connection)
            {
                return db.Query<Post>(sql, new { pageBegin =  pageSize * (page - 1) + 1, pageEnd = page * pageSize });
            }
        }
    }
}
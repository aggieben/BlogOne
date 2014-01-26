using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogOne.Web.Model;

namespace BlogOne.Web.Data
{
    public interface IPostRepository : IRepository<Post>
    {
        Post FindBySlug(string slug);
        Post FindByShortcode(string shortcode);
        IEnumerable<Post> FindPublished(int page = 1, int pageSize = int.MaxValue);
    }
}
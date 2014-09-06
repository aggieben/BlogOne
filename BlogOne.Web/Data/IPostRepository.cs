using BlogOne.Common.Data;
using BlogOne.Web.Model;
using System.Collections.Generic;

namespace BlogOne.Web.Data
{
    public interface IPostRepository : IRepository<Post>
    {
        Post FindBySlug(string slug);
        Post FindByShortcode(string shortcode);
        IEnumerable<Post> FindPublished(int page = 1, int pageSize = int.MaxValue);
    }
}
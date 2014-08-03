using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogOne.Common.Data;
using BlogOne.Shortner.Model;
using BlogOne.Shortner.Models;

namespace BlogOne.Shortner.Data
{
    public interface IShortUrlRepository : IRepository<ShortUrl>
    {
        ShortUrl FindByShortCode(string shortCode);
    }
}

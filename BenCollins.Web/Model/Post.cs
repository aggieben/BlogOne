using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenCollins.Web.Model
{
    public class Post : ModelBase
    {
        public string Title { get; set; }
        public string Body { get; set; }
        // ignore for now: public List<Tag> Tags { get; set; }
    }
}
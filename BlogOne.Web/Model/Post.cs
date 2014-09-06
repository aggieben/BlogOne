using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogOne.Web.Helpers;

namespace BlogOne.Web.Model
{
    public class Post : ModelBase
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public bool Draft { get; set; }
        public string Shortcode { get; private set; }

        public Post()
        {
            if (Shortcode != null) return;
            Shortcode = WebHelper.GetShortcode(10);
            Dirty = true;
        }
    }
}
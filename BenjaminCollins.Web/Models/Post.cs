using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenCollins.Web.Models
{
    public class Post
    {
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
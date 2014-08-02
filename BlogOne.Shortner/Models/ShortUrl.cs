using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogOne.Shortner.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public Guid Sid { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public string ShortCode { get; set; }
    }
}
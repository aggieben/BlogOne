using BlogOne.Shortener.Model;

namespace BlogOne.Shortner.Model
{
    public class ShortUrl : ModelBase
    {
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public string ShortCode { get; set; }
    }
}
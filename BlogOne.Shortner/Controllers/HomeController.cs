using System;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogOne.Shortner.Data;
using BlogOne.Shortner.Model;
using BlogOne.Common.Extensions;

namespace BlogOne.Shortner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortUrlRepository _suRepository;

        public HomeController(IShortUrlRepository suRepository)
        {
            _suRepository = suRepository;
        }

        [Route("{urlId?}"), HttpGet]
        public ActionResult Index(string urlId = null)
        {
            if (String.IsNullOrWhiteSpace(urlId))
            {
                return View();
            }

            var su = _suRepository.FindByShortCode(urlId);
            if (su != null)
            {
                return Redirect(su.Url);
            }

            return HttpNotFound();
        }

        [Route(""), HttpPost]
        public ActionResult New(string url)
        {
            var su = new ShortUrl {Url = url};
            _suRepository.Add(su);

            // code | url | date | enabled
            var tableRow = String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", su.Id.Value.ToBase62(), su.Url, su.CreationDate, su.Enabled);
            return Content(tableRow, "text/html");
            
        }
    }
}
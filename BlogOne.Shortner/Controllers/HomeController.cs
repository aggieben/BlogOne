using System;
using System.Data;
using System.Web.Mvc;
using BlogOne.Shortner.Data;

namespace BlogOne.Shortner.Controllers
{
    [Route("{action=Index}/{urlId?}")]
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
    }
}
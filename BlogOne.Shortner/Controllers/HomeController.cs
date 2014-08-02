using System;
using System.Web.Mvc;

namespace BlogOne.Shortner.Controllers
{
    [Route("{action=Index}/{urlId?}")]
    public class HomeController : Controller
    {
        public HomeController

        [Route("{urlId?}"), HttpGet]
        public ActionResult Index(string urlId = null)
        {
            if (String.IsNullOrWhiteSpace(urlId))
            {
                return View();
            }

            return Redirect(null);
        }
    }
}
using Microsoft.AspNet.Mvc;

namespace BlogOne.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
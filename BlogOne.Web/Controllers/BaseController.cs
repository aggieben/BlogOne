using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace BlogOne.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }   
    }
}
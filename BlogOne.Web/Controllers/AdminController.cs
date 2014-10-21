using System.Threading.Tasks;
using System.Web.Mvc;
using BlogOne.Common.Web.Mvc;
using Microsoft.Owin.Security;

namespace BlogOne.Web.Controllers
{
    [AuthorizeExternal(LoginPath="admin/login")]
    public class AdminController : BaseController
    {
        [Route("admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("admin/login"), AllowAnonymous]
        public ActionResult Login(string returnUrl = "/")
        {
            return Content("login");
        }

        [AllowAnonymous]
        [Route("admin/xlc")]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        return View("Failure");
            //}

            return View("Failure");
        }     
    }
}
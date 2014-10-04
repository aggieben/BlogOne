using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlogOne.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Plus.v1;
using Google.Apis.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BlogOne.Web.Controllers
{
    public class AdminController : Controller
    {
        [Route("admin")]
        public async Task<ActionResult> Index()
        {
            var service = new PlusService(new BaseClientService.Initializer()
            {
                ApplicationName = "BlogOne.v1",
                
            });

            var me = await service.People.Get("me").ExecuteAsync();
            

            return View();
        }

        [Route("admin/login")]
        public ActionResult Login(string returnUrl = "/")
        {
            var loginProviders = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
            if (loginProviders.Count() == 1)
            {
                var p = loginProviders.First();
                return new ChallengeResult(p.AuthenticationType, Url.Action("ExternalLoginCallback", "Admin", new { ReturnUrl = returnUrl}));
            }

            return View("ExternalLogin");
        }

        [AllowAnonymous]
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }        
    }
}
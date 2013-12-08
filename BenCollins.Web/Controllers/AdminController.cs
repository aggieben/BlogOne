using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using BenCollins.Web.Model;
using BenCollins.Web.ViewModel;
using BenCollins.Web.Data;
using BenCollins.Web.Identity;

namespace BenCollins.Web.Controllers
{
    [Authorize]
    public partial class AdminController : Controller
    {
        private readonly IExternalLoginRepository _xlRepository;

        public AdminController(IExternalLoginRepository xlRepository)
        {
            _xlRepository = xlRepository;
            UserManager = new UserManager<User>(new UserStore());
        }

        public UserManager<User> UserManager { get; private set; }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ViewResult Index()
        {
            return View();
        }

        //
        // /Account/ExternalLogin
        [AllowAnonymous]
        [Route("admin/login")]
        public ActionResult ExternalLogin(string returnUrl, string provider = "google")
        {
            // Request a redirect to the external login provider
            var xlTypes = this.ControllerContext.HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
            var requestedXlType = xlTypes.SingleOrDefault(xlt => xlt.AuthenticationType.ToLower() == provider.ToLower());
            if (requestedXlType == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Unsupported Authentication Provider");

            return new ChallengeResult(requestedXlType.AuthenticationType, Url.Action("ExternalLoginCallback", "Admin", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        [Route("admin/xlc")]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var login = _xlRepository.FindByProviderAndKey(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey);
            if (login == null)
            {
                // create new login
                if (_xlRepository.FindAll().Any())
                {
                    throw new HttpException("Unauthorized access");
                }
                else
                {
                    _xlRepository.Add(loginInfo);
                }
            }

            //// Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }

            throw new HttpException("Unexpected failure to find user.");
        }

        //
        // POST: /Account/LogOff
        [Route("admin/logout")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
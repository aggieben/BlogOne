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
        public ViewResult Dashboard()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            //IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            //if (result.Succeeded)
            //{
            //    message = ManageMessageId.RemoveLoginSuccess;
            //}
            //else
            //{
            //    message = ManageMessageId.Error;
            //}
            //return RedirectToAction("Manage", new { Message = message });

            throw new NotImplementedException("disassociate unimplemented.");
        }

        //
        // POST: /Account/ExternalLogin
        [AllowAnonymous]
        [Route("admin/login")]
        public ActionResult ExternalLogin(string provider, string returnUrl)
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
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            //if (loginInfo == null)
            //{
            //    return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            //}
            //var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("Manage");
            //}
            //return RedirectToAction("Manage", new { Message = ManageMessageId.Error });

            throw new NotImplementedException("LinkLoginCallback not implemented.");
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                //var user = new ApplicationUser() { UserName = model.UserName };
                //var result = await UserManager.CreateAsync(user);
                //if (result.Succeeded)
                //{
                //    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                //    if (result.Succeeded)
                //    {
                //        await SignInAsync(user, isPersistent: false);
                //        return RedirectToLocal(returnUrl);
                //    }
                //}
                //AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [Route("admin/logout")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            //var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            //ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            //return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);

            throw new NotImplementedException("RemoveAccountList");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing && UserManager != null)
            //{
            //    UserManager.Dispose();
            //    UserManager = null;
            //}
            base.Dispose(disposing);
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
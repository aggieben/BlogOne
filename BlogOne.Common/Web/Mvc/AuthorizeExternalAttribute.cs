using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BlogOne.Common.Web.Mvc
{
    public class AuthorizeExternalAttribute : AuthorizeAttribute
    {
        public string LoginPath { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var authTypes = filterContext.HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes().ToArray();
            if (authTypes.Length == 1)
            {
                filterContext.Result = new ChallengeResult(authTypes.Single().AuthenticationType, filterContext.HttpContext.Request.Url.ToString());
            }
            else
            {
                filterContext.Result = new RedirectResult(LoginPath);
            }
        }
    }
}

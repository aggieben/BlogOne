using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;
using BlogOne.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Web.Configuration;

namespace BlogOne.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var authSettings = (NameValueCollection)WebConfigurationManager.GetSection("authSettings");

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = authSettings[AppSettingsKeys.GoogleClientId],
                ClientSecret = authSettings[AppSettingsKeys.GoogleClientSecret],
                CallbackPath = PathString.FromUriComponent("/admin/xlc"),
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        return Task.Factory.StartNew(() => Trace.WriteLine("authenticated."));

                    }
                }
            });
        }
    }
}
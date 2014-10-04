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
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/admin/login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            if (bool.Parse(WebConfigurationManager.AppSettings[AppSettingsKeys.Initialized]))
            {
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
                {
                    ClientId = WebConfigurationManager.AppSettings[AppSettingsKeys.GoogleClientId],
                    ClientSecret = WebConfigurationManager.AppSettings[AppSettingsKeys.GoogleClientSecret],
                    Provider = new GoogleOAuth2AuthenticationProvider
                    {
                        OnAuthenticated = (context) =>
                        {
                            context.
                        }
                    }
                });
            }
        }
    }
}
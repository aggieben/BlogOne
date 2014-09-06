using Microsoft.Owin;
using Microsoft.Owin.Diagnostics;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogOne.Web.Startup))]
namespace BlogOne.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage(new ErrorPageOptions
            {
                ShowCookies = true,
                ShowEnvironment = true,
                ShowExceptionDetails = true,
                ShowHeaders = true,
                ShowQuery = true,
                ShowSourceCode = true,
            });

            ConfigureAuth(app);
        }
    }
}

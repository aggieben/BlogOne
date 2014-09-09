using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
//using Microsoft.AspNet.Security.Cookies;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
//using Microsoft.AspNet.Security.Google;

namespace BlogOne.Web
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            ConfigureServices(app);
            ConfigureRoutes(app);
        }

        public void ConfigureServices(IBuilder app)
        {
            app.UseServices(services =>
            {
                services.AddMvc();
            });
        }

        public void ConfigureRoutes(IBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureAuth(IBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{                
            //    LoginPath = new PathString("/login"),
            //    LogoutPath = new PathString("/logout")
            //});

            //var googleOptions = new GoogleAuthenticationOptions
            //{

            //};
        }
    }
}
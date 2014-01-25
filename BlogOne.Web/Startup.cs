using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogOne.Web.Startup))]
namespace BlogOne.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

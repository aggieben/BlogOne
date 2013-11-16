using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BenjaminCollins.Web.Startup))]
namespace BenjaminCollins.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

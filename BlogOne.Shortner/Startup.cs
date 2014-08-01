using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogOne.Shortner.Startup))]
namespace BlogOne.Shortner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

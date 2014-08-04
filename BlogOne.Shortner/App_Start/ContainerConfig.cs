using BlogOne.Common.Cache;
using BlogOne.Common.Data;
using BlogOne.Shortner.Controllers;
using BlogOne.Shortner.Data;
using Munq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BlogOne.Shortner.App_Start
{
    public static class ContainerConfig
    {
        public static void RegisterContainer()
        {
            DependencyResolver.SetResolver(new Common.Web.Mvc.DependencyResolver(BuildContainer()));
        }

        public static IocContainer BuildContainer()
        {
            var ioc = new IocContainer();

            // caches
            ioc.Register(c => new CacheProvider(HttpContext.Current.Cache));
            ioc.Register(c => c.Resolve<CacheProvider>().GetCache(WebConfigurationManager.AppSettings["redisConfig"]));

            // database
            ioc.Register<IDbConnectionFactory>(c => new SqlConnectionFactory(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            // repositories
            ioc.Register<IShortUrlRepository, ShortUrlRepository>();
            //ioc.Register<IExternalLoginRepository, ExternalLoginRepository>();
            //ioc.Register<IPostRepository, PostRepository>();

            // controllers
            ioc.Register(c => new HomeController(c.Resolve<IShortUrlRepository>()));
            //ioc.Register<AdminController>(c => new AdminController(c.Resolve<IExternalLoginRepository>(), c.Resolve<IPostRepository>()));
            //ioc.Register<PostController>(c => new PostController(c.Resolve<IPostRepository>()));

            return ioc;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using BlogOne.Common.Cache;
using BlogOne.Common.Data;
using Munq;

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
            //ioc.Register<IExternalLoginRepository, ExternalLoginRepository>();
            //ioc.Register<IPostRepository, PostRepository>();

            // controllers
            //ioc.Register<HomeController>(c => new HomeController(c.Resolve<IPostRepository>()));
            //ioc.Register<AdminController>(c => new AdminController(c.Resolve<IExternalLoginRepository>(), c.Resolve<IPostRepository>()));
            //ioc.Register<PostController>(c => new PostController(c.Resolve<IPostRepository>()));

            return ioc;
        }
    }
}
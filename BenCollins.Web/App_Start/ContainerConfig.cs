using System.Web.Configuration;
using System.Web.Mvc;
using BenCollins.Web.Controllers;
using BenCollins.Web.Data;
using Munq;

namespace BenCollins.Web {
	public static class ContainerConfig {
		public static void RegisterContainer() 
        {
			DependencyResolver.SetResolver(new Mvc.DependencyResolver(BuildContainer()));
		}

        public static IocContainer BuildContainer()
        {
            var ioc = new IocContainer();

            ioc.Register<IDbConnectionFactory>(c => new SqlConnectionFactory(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            // repositories
            ioc.Register<IExternalLoginRepository, ExternalLoginRepository>();
            ioc.Register<IPostRepository, PostRepository>();

            // controllers
            ioc.Register<AdminController>(
                c =>
                {
                    var xlr = c.Resolve(typeof(IExternalLoginRepository)) as IExternalLoginRepository;
                    return new AdminController(xlr);
                });

            ioc.Register<PostController>(
                c =>
                {
                    var pr = c.Resolve(typeof(IPostRepository)) as IPostRepository;
                    return new PostController(pr);
                });

            return ioc;
        }
	}
}
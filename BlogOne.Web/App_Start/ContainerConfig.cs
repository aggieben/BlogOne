using System.Web.Configuration;
using System.Web.Mvc;
using BlogOne.Common.Data;
using BlogOne.Web.Controllers;
using BlogOne.Web.Data;
using Munq;

namespace BlogOne.Web {
	public static class ContainerConfig {
		public static void RegisterContainer() 
        {
			DependencyResolver.SetResolver(new Common.Web.Mvc.DependencyResolver(BuildContainer()));
		}

        public static IocContainer BuildContainer()
        {
            var ioc = new IocContainer();

            // repositories
            ioc.Register<IPostRepository, PostRepository>();

            // controllers            

            return ioc;
        }
	}
}
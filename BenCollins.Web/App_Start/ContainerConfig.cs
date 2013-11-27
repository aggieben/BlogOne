using System.Web.Mvc;
using Munq.MVC3;
using BenCollins.Web.Data;

namespace BenCollins.Web {
	public static class ContainerConfig {
		public static void RegisterContainer() 
        {
			DependencyResolver.SetResolver(new MunqDependencyResolver());

            RegisterDependencies();
		}

        public static void RegisterDependencies()
        {
            var ioc = MunqDependencyResolver.Container;

            ioc.Register<IExternalLoginRepository, ExternalLoginRepository>();
        }
	}
}
 


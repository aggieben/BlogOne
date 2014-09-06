using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogOne.Web.Mvc
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly Munq.IocContainer _container;
        
        public DependencyResolver(Munq.IocContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.CanResolve(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.CanResolve(serviceType) ? _container.ResolveAll(serviceType) : new object[] { };
        }
    }
}
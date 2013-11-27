using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BenCollins.Web.Mvc
{
    public class MunqControllerFactory : DefaultControllerFactory
    {
        private readonly Munq.IocContainer _container;

        public MunqControllerFactory(Munq.IocContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            var cntl = _container.Resolve(controllerType) as IController;

            return cntl ?? base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
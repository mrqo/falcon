using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.EntityComponentModel;

namespace Falcon.Engine.Implementation.EntityComponentModel
{
    public class ComponentResolverFactory : IComponentResolverFactory
    {
        public IComponentResolver Create()
        {
            return new ComponentResolver();
        }
    }
}

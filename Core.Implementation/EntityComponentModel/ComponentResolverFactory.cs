using System;
using System.Collections.Generic;
using System.Text;
using Engine.EntityComponentModel;

namespace Engine.Implementation.EntityComponentModel
{
    public class ComponentResolverFactory : IComponentResolverFactory
    {
        public IComponentResolver Create()
        {
            return new ComponentResolver();
        }
    }
}

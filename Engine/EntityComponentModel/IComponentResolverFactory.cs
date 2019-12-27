using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.EntityComponentModel
{
    public interface IComponentResolverFactory
    {
        IComponentResolver Create();
    }
}

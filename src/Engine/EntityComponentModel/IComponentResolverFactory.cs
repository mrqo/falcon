using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.EntityComponentModel
{
    public interface IComponentResolverFactory
    {
        IComponentResolver Create();
    }
}

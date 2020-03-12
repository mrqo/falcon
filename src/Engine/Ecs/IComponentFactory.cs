using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;

namespace Falcon.Engine.Ecs
{
    public interface IComponentFactory
    {
        Component Create(string componentName);

        TComponent Create<TComponent>() where TComponent : Component;
    }
}

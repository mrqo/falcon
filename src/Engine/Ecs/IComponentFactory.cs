using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;

namespace Falcon.Engine.Ecs
{
    public interface IComponentFactory
    {
        Component Create(Type componentType, Entity entity);

        TComponent Create<TComponent>(Entity entity) where TComponent : Component;
    }
}

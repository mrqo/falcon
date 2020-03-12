using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Ecs
{
    public interface IComponentResolver
    {
        IComponentResolver With(Component component);

        TComponent FindComponent<TComponent>() where TComponent : Component;

        IReadOnlyCollection<Component> Components { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.EntityComponentModel
{
    public interface IComponentResolver
    {
        void AddComponent(Component component);

        TComponent FindComponent<TComponent>() where TComponent : Component;

        IReadOnlyCollection<Component> Components { get; }
    }
}

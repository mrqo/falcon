using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Ecs
{
    public interface IComponentResolver
    {
        public Entity Entity { get; set; }
        
        // #TODO: Implement OnAdd event.
        IComponentResolver Add<TComponent>() where TComponent : Component;

        Component Find(Type t);
        
        TComponent Find<TComponent>() where TComponent : Component;

        IReadOnlyCollection<Component> Components { get; }
    }
}

using System;
using System.Collections.Generic;

namespace Falcon.Engine.Ecs
{
    public interface IComponentProvider
    {
        bool Add<TComponent>(Entity entity) where TComponent : Component;

        Component Find(int componentId);
        TComponent Find<TComponent>(int componentId) where TComponent : Component;
        
        Component FindByEntity(Type componentType, int entityId);

        TComponent FindByEntity<TComponent>(int entityId) where TComponent : Component;
        
        Component FindByEntity(Type componentType, Entity entity);

        TComponent FindByEntity<TComponent>(Entity entity) where TComponent : Component;

        IEnumerable<Component> List(Entity entity);
    }
}
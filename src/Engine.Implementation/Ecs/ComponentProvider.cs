using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class ComponentProvider : IComponentProvider
    {
        private readonly IComponentFactory _componentFactory;
        
        private readonly Dictionary<int, List<Component>> _entityIdsToComponents
            = new Dictionary<int, List<Component>>();
        
        private readonly Dictionary<int, Component> _idsToComponents 
            = new Dictionary<int, Component>();
        
        public ComponentProvider(IComponentFactory componentFactory)
        {
            _componentFactory = componentFactory;
        }

        public bool Add<TComponent>(Entity entity) 
            where TComponent : Component
        {
            if (entity == null)
            {
                return false;
            }
            
            var component = _componentFactory.Create<TComponent>(entity);
            var entityId = entity.GetHashCode();
            
            if (!_entityIdsToComponents.ContainsKey(entityId))
            {
                _entityIdsToComponents.Add(entityId, new List<Component>());
            }
            
            _entityIdsToComponents[entityId].Add(component);
            _idsToComponents[component.GetHashCode()] = component;
            return true;
        }

        public Component Find(int componentId) =>
            _idsToComponents[componentId];

        public TComponent Find<TComponent>(int componentId)
            where TComponent : Component =>
            Find(componentId) as TComponent;
        
        public Component FindByEntity(Type componentType, int entityId) =>
            componentType == null
                ? null
                : _entityIdsToComponents[entityId]
                        ?.FirstOrDefault(comp => comp.GetType() == componentType);


        public TComponent FindByEntity<TComponent>(int entityId) 
            where TComponent : Component =>
            FindByEntity(typeof(TComponent), entityId) as TComponent;

        public Component FindByEntity(Type componentType, Entity entity) =>
            entity == null
                ? null
                : FindByEntity(componentType, entity.GetHashCode());

        public TComponent FindByEntity<TComponent>(Entity entity)
            where TComponent : Component =>
            FindByEntity(typeof(TComponent), entity) as TComponent;

        public IEnumerable<Component> List(Entity entity) =>
            entity == null
                ? new List<Component>()
                : _entityIdsToComponents[entity.GetHashCode()];
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class ComponentResolver : IComponentResolver
    {
        public Entity Entity { get; set; }
        
        private IComponentFactory _componentFactory;
        
        private List<Component> _components = new List<Component>();
        
        public IReadOnlyCollection<Component> Components => _components;

        public ComponentResolver(IComponentFactory compFactory)
        {
            _componentFactory = compFactory;
        }

        public IComponentResolver Add<TComponent>()
            where TComponent : Component
        {
            if (Entity == null)
            {
                throw new ArgumentException("Entity is not set. Cannot assign component to null entity.");
            }
            
            var component = _componentFactory.Create<TComponent>();
            component.Entity = Entity;
            
            _components.Add(component);
            return this;
        }

        public Component Find(Type t) =>
            _components.FirstOrDefault(comp => comp.GetType() == t);

        public TComponent Find<TComponent>() where TComponent : Component =>
            Find(typeof(TComponent)) as TComponent;
    }
}

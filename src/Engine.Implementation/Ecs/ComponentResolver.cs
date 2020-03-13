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
        private List<Component> _components;

        public IReadOnlyCollection<Component> Components => _components;

        public ComponentResolver()
        {
            _components = new List<Component>();
        }

        public IComponentResolver With(Component component)
        {
            _components.Add(component);
            return this;
        }

        public Component FindComponent(Type t) =>
            _components.FirstOrDefault(comp => comp.GetType() == t);

        public TComponent FindComponent<TComponent>() where TComponent : Component =>
            FindComponent(typeof(TComponent)) as TComponent;
    }
}

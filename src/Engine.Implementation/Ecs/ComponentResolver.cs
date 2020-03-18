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

        private readonly IComponentProvider _componentProvider;
        
        public IReadOnlyCollection<Component> Components 
            => _componentProvider.List(Entity) as IReadOnlyCollection<Component>;

        public ComponentResolver(IComponentProvider componentProvider)
        {
            _componentProvider = componentProvider;
        }

        public IComponentResolver Add<TComponent>()
            where TComponent : Component
        {
            if (Entity == null)
            {
                throw new ArgumentException("Entity is not set. Cannot assign component to null entity.");
            }

            _componentProvider.Add<TComponent>(Entity);
            return this;
        }

        public Component Find(Type t) =>
            _componentProvider.FindByEntity(t, Entity);

        public TComponent Find<TComponent>()
            where TComponent : Component =>
            _componentProvider.FindByEntity<TComponent>(Entity);
    }
}

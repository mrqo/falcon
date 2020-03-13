using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class EntityQueryBuilder : IEntityQueryBuilder
    {
        private IEntityProvider _entityProvider;

        private List<Type> _componentTypes = new List<Type>();
        
        public IEntityQueryBuilder Init(IEntityProvider provider)
        {
            _entityProvider = provider;
            _componentTypes = new List<Type>();

            return this;
        }
        
        public IEntityQueryBuilder With<TComponent>()
        {
            _componentTypes.Add(typeof(TComponent));
            return this;
        }

        public IEnumerable<Entity> Get() =>
            _entityProvider.Entities.Where(e =>
                _componentTypes.TrueForAll(compType => e.FindComponent(compType) != null));
    }
}
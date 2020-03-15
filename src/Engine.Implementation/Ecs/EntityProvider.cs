using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class EntityProvider : IEntityProvider
    {
        private IEntityQueryBuilder _queryBuilder;

        private IEntityFactory _entityFactory;
        
        // #TODO: Change it to Dictionary, to reduce linear search time in "WithComponents"
        private HashSet<Entity> _entities = new HashSet<Entity>();

        private Dictionary<Type, HashSet<int>> _compTypesToEntities = new Dictionary<Type, HashSet<int>>();
        
        public IEnumerable<Entity> Entities => _entities;
        
        public EntityProvider(
            IEntityQueryBuilder queryBuilder,
            IEntityFactory entityFactory)
        {
            _queryBuilder = queryBuilder;
            _entityFactory = entityFactory;
        }

        public TEntity Create<TEntity>() 
            where TEntity : Entity
        {
            var entity = _entityFactory.Create<TEntity>();
            
            Add(entity);
            
            return entity;
        }

        public void Add<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            // #TODO: Subscribe to entity's ComponentResolver OnAdd event
            
            entity.Components
                .Select(comp => comp.GetType())
                .ToList()
                .ForEach(ct =>
                {
                    if (!_compTypesToEntities.ContainsKey(ct))
                    {
                        _compTypesToEntities.Add(ct, new HashSet<int> { entity.GetHashCode() });
                    }
                    else
                    {
                        _compTypesToEntities[ct].Add(entity.GetHashCode());
                    }
                });
            
            _entities.Add(entity);
        }

        public Entity Get(int entityId) =>
            Get<Entity>(entityId);
        
        public TEntity Get<TEntity>(int entityId)
            where TEntity : Entity =>
            _entities
                .FirstOrDefault(entity => entity.GetHashCode() == entityId) as TEntity;
        
        public IEntityQueryBuilder Query() =>
            _queryBuilder.Init(this);

        public IEnumerable<Entity> WithComponents(IEnumerable<Type> componentTypes) =>
            componentTypes
                .Select(ct => _compTypesToEntities.GetValueOrDefault(ct))
                .Where(ct => ct != null)
                .Aggregate((acc, next) =>
                {
                    acc.IntersectWith(next);
                    return acc;
                })
                .Select(id => _entities.FirstOrDefault(e => e.GetHashCode() == id));
    }
}
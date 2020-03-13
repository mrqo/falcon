using System.Collections;
using System.Collections.Generic;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class EntityProvider : IEntityProvider
    {
        private IEntityQueryBuilder _queryBuilder;

        private IEntityFactory _entityFactory;
        
        private List<Entity> _entities = new List<Entity>();

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
            _entities.Add(entity);

            return entity;
        }
        
        public IEntityQueryBuilder Query() =>
            _queryBuilder.Init(this);
    }
}
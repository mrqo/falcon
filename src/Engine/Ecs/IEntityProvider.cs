using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Ecs
{
    public interface IEntityProvider
    {
        IEnumerable<Entity> Entities { get; }

        TEntity Create<TEntity>() where TEntity : Entity;
        
        IEntityQueryBuilder Query();
    }
}

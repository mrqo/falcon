using System.Collections;
using System.Collections.Generic;

namespace Falcon.Engine.Ecs
{
    public interface IEntityQueryBuilder
    {
        IEntityQueryBuilder Init(IEntityProvider provider);
        
        IEntityQueryBuilder With<TComponent>();

        IEnumerable<Entity> Get();
    }
}
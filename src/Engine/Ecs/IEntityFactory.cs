using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Ecs
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>() where TEntity : Entity;
    }
}

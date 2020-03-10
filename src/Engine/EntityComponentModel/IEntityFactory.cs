using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.EntityComponentModel
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>() where TEntity : Entity;
    }
}

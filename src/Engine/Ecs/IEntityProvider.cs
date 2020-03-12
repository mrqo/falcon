using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Ecs
{
    public interface IEntityProvider
    {
        IEnumerable<Entity> Entities { get; }
    }
}

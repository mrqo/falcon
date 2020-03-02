using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.EntityComponentModel
{
    public interface IEntityProvider
    {
        IEnumerable<Entity> Entities { get; }
    }
}

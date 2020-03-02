using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.EntityComponentModel
{
    public interface IEntityProvider
    {
        IEnumerable<Entity> Entities { get; }
    }
}

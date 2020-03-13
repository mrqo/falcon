using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.World
{
    public class Level : Entity
    {
        public Level(EntityDeps deps)
            : base(deps)
        {
            // #TODO: Add components related to level.
        }
    }
}

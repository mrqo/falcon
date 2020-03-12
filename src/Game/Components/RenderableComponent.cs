using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Ecs;

namespace Falcon.Game.Components
{
    class RenderableComponent : Component
    {
        public override void Update()
        {
            var walkComp = Entity.FindComponent<WalkComponent>();
        }
    }
}

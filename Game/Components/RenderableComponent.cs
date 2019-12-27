using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.EntityComponentModel;

namespace Game.Components
{
    class RenderableComponent : Component
    {
        public override void Update()
        {
            var walkComp = Entity.FindComponent<WalkComponent>();
        }
    }
}

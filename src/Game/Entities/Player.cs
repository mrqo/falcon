using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.Communication;
using Engine.EntityComponentModel;
using Falcon.Game.Components;

namespace Falcon.Game.Entities
{
    class Player : Entity
    {
        public void ConfigDefault(IComponentFactory compFactory)
        {
            AddComponent(compFactory.Create<JumpComponent>());
            AddComponent(compFactory.Create<WalkComponent>());
            AddComponent(compFactory.Create<ControllerComponent>());
            AddComponent(compFactory.Create<RenderableComponent>());
        }
    }
}

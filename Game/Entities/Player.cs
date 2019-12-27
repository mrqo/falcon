using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.Communication;
using Game.Components;
using Engine.EntityComponentModel;

namespace Game.Entities
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

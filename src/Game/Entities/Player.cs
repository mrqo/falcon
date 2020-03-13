using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;
using Falcon.Game.Components;

namespace Falcon.Game.Entities
{
    class Player : Entity
    {
        public Player(EntityDeps deps)
        : base(deps)
        {
            ComponentResolver
                .Add<JumpComponent>()
                .Add<WalkComponent>()
                .Add<ControllerComponent>()
                .Add<RenderableComponent>();
        }
    }
}

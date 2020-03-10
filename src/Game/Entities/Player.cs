using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;
using Falcon.Game.Components;

namespace Falcon.Game.Entities
{
    class Player : Entity
    {
        public Player(
            INotificationHub notificationHub,
            IComponentResolver componentResolver,
            IComponentFactory compFactory)
        : base(notificationHub, componentResolver)
        {
            this.With(compFactory.Create<JumpComponent>())
                .With(compFactory.Create<WalkComponent>())
                .With(compFactory.Create<ControllerComponent>())
                .With(compFactory.Create<RenderableComponent>());
        }
    }
}

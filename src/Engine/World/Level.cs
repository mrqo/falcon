using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.World
{
    public class Level : Entity
    {
        public Level(
            INotificationHub notificationHub,
            IComponentResolver componentResolver,
            IComponentFactory compFactory)
        : base(notificationHub, componentResolver)
        {
            // #TODO: Add components related to level.
        }
    }
}

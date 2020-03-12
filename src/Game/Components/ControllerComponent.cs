using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;

namespace Falcon.Game.Components
{
    class ControllerComponent : Component
    {
        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                Notify("KeyPressed", Console.ReadKey(true).Key);
            }
        }
    }
}

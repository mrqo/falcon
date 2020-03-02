using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.Communication;
using Engine.EntityComponentModel;

namespace Game.Components
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

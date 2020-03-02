using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;

namespace Falcon.Game.Components
{
    class WalkComponent : Component
    {
        [CoProperty]
        public int Speed { get; set; }

        [CoProperty]
        public ConsoleKey ForwardKey { get; set; }

        [CoProperty]
        public ConsoleKey BackwardKey { get; set; }

        public override void RegisterSubscriptions(INotificationHub hub)
        {
            base.RegisterSubscriptions(hub);

            hub.Subscribe("KeyPressed", OnKeyPressed);
        }

        public override void Update()
        {

        }

        protected void OnKeyPressed(object msg, object sender)
        {
            if ((ConsoleKey) msg == ForwardKey)
            {
                Console.WriteLine("Moving forwards");
            }

            if ((ConsoleKey) msg == BackwardKey)
            {
                Console.WriteLine("Moving backwards");
            }
        }
    }
}

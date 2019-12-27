﻿using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.Communication;
using Engine.EntityComponentModel;

namespace Game.Components
{
    class WalkComponent : Component
    {
        public int Speed { get; set; }

        public ConsoleKey ForwardKey { get; set; }

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
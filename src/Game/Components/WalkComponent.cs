using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;

namespace Falcon.Game.Components
{
    public class WalkComponent : Component
    {
        [CoProperty]
        public int Speed { get; set; }

        [CoProperty]
        public ConsoleKey ForwardKey { get; set; }

        [CoProperty]
        public ConsoleKey BackwardKey { get; set; }
    }
}

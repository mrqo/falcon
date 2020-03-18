using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;

namespace Falcon.Game.Components
{
    public class JumpComponent : Component
    {
        public enum JumpState
        {
            Idle,
            InAir
        }
        
        public JumpState State { get; set; }
        
        [CoProperty]
        public double MaxHeight { get; set; } = 15.0;

        [CoProperty]
        public double FloatingTime { get; set; } = 10.0;

        [CoProperty]
        public ConsoleKey JumpKey { get; set; }

        [CoProperty]
        public double CurHeight { get; set; }

        [CoProperty]
        public double CurTime { get; set; }

        public bool IsIdle => State == JumpState.Idle;

        public bool IsInAir => State == JumpState.InAir;

        public JumpComponent(Entity entity)
            : base(entity)
        {
        }
        
    }
}

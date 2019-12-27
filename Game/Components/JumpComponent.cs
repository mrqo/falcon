﻿using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Engine;
using Engine.Communication;
using Engine.EntityComponentModel;

namespace Game.Components
{
    class JumpComponent : Component
    {
        enum State
        {
            Idle,
            InAir
        }

        public double MaxHeight { get; set; } = 15.0;

        public double FloatingTime { get; set; } = 10.0;

        public ConsoleKey JumpKey { get; set; }

        public double CurHeight { get; private set; } = 0.0;

        public double CurTime { get; private set; } = 0.0;

        private State _state = State.Idle;

        public override void RegisterSubscriptions(INotificationHub hub)
        {
            base.RegisterSubscriptions(hub);

            hub.Subscribe("KeyPressed", OnKeyPressed);
        }

        public override void Update()
        {
            UpdateJumpTime();
            UpdateHeight();

            if (_state == State.InAir)
            {
                Console.WriteLine($"Height: {CurHeight}");
            }
        }

        protected void UpdateJumpTime()
        {
            if (_state == State.Idle)
            {
                return;
            }

            CurTime += Dt;

            if (CurTime > FloatingTime)
            {
                CurTime = 0;
                _state = State.Idle;
            }
        }

        protected void UpdateHeight()
        {
            CurHeight = Math.Sin(CurTime / FloatingTime * Math.PI) * MaxHeight;
        }

        protected void OnKeyPressed(object msg, object sender)
        {
            if ((ConsoleKey)msg == JumpKey)
            {
                if (_state == State.Idle)
                {
                    _state = State.InAir;
                }
            }
        }
    }
}
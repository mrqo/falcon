using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Ecs;
using Falcon.Game.Components;

namespace Falcon.Game.Systems
{
    public class PlayerMovementSystem : ISystem
    {
        private readonly IEntityProvider _entityProvider;

        public bool IsActive { get; set; } = true;
        
        public double Dt { get; set; }
        
        public PlayerMovementSystem(IEntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
        }
        
        public void Step()
        {
            Target.ForEach(entity =>
            {
                var walkComponent = entity.ComponentResolver.Find<WalkComponent>();
                var jumpComponent = entity.ComponentResolver.Find<JumpComponent>();
                
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == walkComponent.ForwardKey)
                    {
                        Console.WriteLine("Moving forwards");
                    }

                    if (key == walkComponent.BackwardKey)
                    {
                        Console.WriteLine("Moving backwards");
                    }

                    if (key == jumpComponent.JumpKey)
                    {
                        if (jumpComponent.IsIdle)
                        {
                            jumpComponent.State = JumpComponent.JumpState.InAir;
                        }
                    }
                }

                UpdateJumpTime(jumpComponent);
                UpdateHeight(jumpComponent);

                if (jumpComponent.IsInAir)
                {
                    Console.WriteLine($"Height: {jumpComponent.CurHeight}");
                }
            });
        }
        
        private void UpdateJumpTime(JumpComponent jumpComponent)
        {
            if (jumpComponent.IsIdle)
            {
                return;
            }

            if (jumpComponent.IsInAir)
            {
                jumpComponent.CurTime += Dt;
            }

            if (jumpComponent.CurTime > jumpComponent.FloatingTime)
            {
                jumpComponent.CurTime = 0;
                jumpComponent.State = JumpComponent.JumpState.Idle;
            }
        }
        
        private void UpdateHeight(JumpComponent jumpComponent)
        {
            jumpComponent.CurHeight = Math.Sin(
                jumpComponent.CurTime / jumpComponent.FloatingTime * Math.PI) * jumpComponent.MaxHeight;
        }
        
        private List<Entity> Target => _entityProvider
            .Query()
            .With<WalkComponent>()
            .With<JumpComponent>()
            .Get()
            .ToList();
    }
}
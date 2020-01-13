using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.Communication;
using Engine.EntityComponentModel;
using Engine.Execution;
using Game.Components;
using Game.Entities;

namespace Game
{
    public class Game : IExecutionTarget
    {
        protected List<Entity> Entities { get; set; }

        protected INotificationHub NotificationHub { get; private set; }

        protected IComponentFactory ComponentFactory { get; private set; }

        protected IComponentResolverFactory ComponentResolverFactory { get; private set; }
        public Game()
        {
            Entities = new List<Entity>();
        }

        public void Init(
            INotificationHub notificationHub, 
            IComponentFactory componentFactory,
            IComponentResolverFactory componentResolverFactory)
        {
            NotificationHub = notificationHub;
            ComponentFactory = componentFactory;
            ComponentResolverFactory = componentResolverFactory;

            CreateEntities();
        }
        
        public void Update(float dt)
        {
            foreach (var entity in Entities)
            {
                entity.Update(dt);
            }
        }

        public void Perform()
        {
            // #TODO: Render logic here
        }

        protected void CreateEntities()
        {
            var entity = new Player();
            entity.Init(NotificationHub, ComponentResolverFactory.Create());
            entity.ConfigDefault(ComponentFactory);
            
            var walkCo = entity.FindComponent<WalkComponent>();
            walkCo.ForwardKey = ConsoleKey.W;
            walkCo.BackwardKey = ConsoleKey.S;

            var jumpCo = entity.FindComponent<JumpComponent>();
            jumpCo.JumpKey = ConsoleKey.Spacebar;

            Entities.Add(entity);
        }
    }
}

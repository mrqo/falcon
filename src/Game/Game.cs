using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Execution;
using Falcon.Engine.Networking;
using Falcon.Game.Components;
using Falcon.Game.Entities;

namespace Falcon.Game
{
    public class Game 
        : IExecutionTarget
        , IEntityProvider
    {
        protected List<Entity> _entities;

        public IEnumerable<Entity> Entities => _entities;

        protected INotificationHub NotificationHub { get; private set; }

        protected IComponentFactory ComponentFactory { get; private set; }

        protected IComponentResolverFactory ComponentResolverFactory { get; private set; }

        protected IStateManager StateManager { get; private set; }
        
        public Game()
        {
            _entities = new List<Entity>();
        }

        public void Init(
            INotificationHub notificationHub, 
            IComponentFactory componentFactory,
            IComponentResolverFactory componentResolverFactory,
            IStateManager stateManager)
        {
            NotificationHub = notificationHub;
            ComponentFactory = componentFactory;
            ComponentResolverFactory = componentResolverFactory;
            StateManager = stateManager;

            CreateEntities();
        }
        
        public void Update(float dt)
        {
            foreach (var entity in _entities)
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

            _entities.Add(entity);

            Task.Run(async () =>
            {
                var msg = await StateManager.Update(entity);
                Console.WriteLine(msg);
            });
        }
    }
}

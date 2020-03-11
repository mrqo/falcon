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
using Ninject;

namespace Falcon.Game
{
    public class Game 
        : IExecutionTarget
        , IEntityProvider
    {
        private List<Entity> _entities = new List<Entity>();

        public IEnumerable<Entity> Entities => _entities;

        protected IStateManager StateManager { get; }
        
        protected IEntityFactory EntityFactory { get; }

        protected IKernel Kernel { get; }

        public Game(
            IStateManager stateManager, 
            IEntityFactory entityFactory,
            IKernel kernel)
        {
            StateManager = stateManager;
            EntityFactory = entityFactory;
            Kernel = kernel;
        }

        public void Start()
        {
            CreateEntities();
        }

        public void Update(float dt)
        {
            _entities.ForEach(entity => entity.Update(dt));
        }

        public void Perform()
        {
            // #TODO: Render logic here
        }

        public void RegisterTypes()
        {
            Kernel.Bind<Player>().To<Player>();
        }

        protected void CreateEntities()
        {
            var entity = EntityFactory.Create<Player>();

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

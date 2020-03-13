using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Falcon.Engine;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;
using Falcon.Engine.Execution;
using Falcon.Engine.Networking;
using Falcon.Game.Components;
using Falcon.Game.Entities;
using Ninject;

namespace Falcon.Game
{
    public class Game : IExecutionTarget
    {
        private readonly IStateManager _stateManager;
        
        private readonly IEntityProvider _entityProvider;

        private readonly IKernel _kernel;

        public Game(
            IStateManager stateManager,
            IEntityProvider entityProvider,
            IKernel kernel)
        {
            _stateManager = stateManager;
            _entityProvider = entityProvider;
            _kernel = kernel;
        }

        public void Start()
        {
            CreateEntities();
        }

        public void Update(float dt) =>
            _entityProvider.Entities
                .ToList()
                .ForEach(entity => entity.Update(dt));

        public void Perform()
        {
            // #TODO: Render logic here
        }

        public void RegisterTypes()
        {
            _kernel.Bind<Player>().ToSelf();
        }

        protected void CreateEntities()
        {
            var entity = _entityProvider.Create<Player>();

            var walkCo = entity.FindComponent<WalkComponent>();
            walkCo.ForwardKey = ConsoleKey.W;
            walkCo.BackwardKey = ConsoleKey.S;

            var jumpCo = entity.FindComponent<JumpComponent>();
            jumpCo.JumpKey = ConsoleKey.Spacebar;
            
            Task.Run(async () =>
            {
                var msg = await _stateManager.Update(entity);
                Console.WriteLine(msg);
            });
        }
    }
}

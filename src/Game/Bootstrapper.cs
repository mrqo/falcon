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
using Falcon.Game.Systems;
using Ninject;

namespace Falcon.Game
{
    public class Bootstrapper : IBootstrapper
    {
        private readonly IEntityProvider _entityProvider;

        private readonly IKernel _kernel;
        
        public Bootstrapper(
            IEntityProvider entityProvider,
            IKernel kernel)
        {
            _entityProvider = entityProvider;
            _kernel = kernel;
        }

        public void Start()
        {
            CreateEntities();
        }
        
        public void RegisterTypes()
        {
            _kernel.Bind<Player>().ToSelf();
        }

        private void CreateEntities()
        {
            var entity = _entityProvider.Create<Player>();
            ConfigureComponent(entity.ComponentResolver.Find<WalkComponent>());
            ConfigureComponent(entity.ComponentResolver.Find<JumpComponent>());
        }
        
        private static void ConfigureComponent(WalkComponent walkComponent)
        {
            walkComponent.ForwardKey = ConsoleKey.W;
            walkComponent.BackwardKey = ConsoleKey.S;
        }

        private static void ConfigureComponent(JumpComponent jumpComponent)
        {
            jumpComponent.JumpKey = ConsoleKey.Spacebar;
        }
    }
}

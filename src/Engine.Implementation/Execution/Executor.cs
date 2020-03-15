using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;
using Falcon.Engine.Execution;
using Falcon.Engine.Networking;
using Falcon.Engine.World;
using Ninject;

namespace Falcon.Engine.Implementation.Execution
{
    public class Executor : IExecutor
    {
        private readonly IKernel _kernel;
        
        private readonly IBootstrapper _bootstrapper;

        private readonly ISystemManager _systemManager;
        
        public Executor(
            IKernel kernel, 
            IBootstrapper bootstrapper,
            ISystemManager systemManager)
        {
            _kernel = kernel;
            _bootstrapper = bootstrapper;
            _systemManager = systemManager;
            
            RegisterDefaultTypes();
            RegisterGameTypes();
            Bootstrap();
        }

        private void RegisterDefaultTypes()
        {
            _kernel.Bind<Level>().To<Level>();
        }

        private void RegisterGameTypes() =>
            _bootstrapper.RegisterTypes();

        private void Bootstrap() =>
            _bootstrapper.Start();
        
        public void Step(float dt) =>
            _systemManager.Execute(dt);
    }
}

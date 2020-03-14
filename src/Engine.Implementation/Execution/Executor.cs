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
        private readonly IExecutionTarget _target;

        private readonly IKernel _kernel;
        
        public Executor(IKernel kernel, IExecutionTarget target)
        {
            _kernel = kernel;
            _target = target;

            RegisterDefaultTypes();
            RegisterExecutionTargetTypes();
            StartExecutionTarget();
        }

        private void RegisterDefaultTypes()
        {
            _kernel.Bind<Level>().To<Level>();
        }

        private void RegisterExecutionTargetTypes() =>
            _target.RegisterTypes();

        private void StartExecutionTarget() =>
            _target.Start();
        
        public void Step(float dt) =>
            _target.Step(dt);
    }
}

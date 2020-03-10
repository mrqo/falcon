using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Execution;
using Falcon.Engine.Networking;
using Falcon.Engine.World;
using Ninject;

namespace Falcon.Engine.Implementation.Execution
{
    public class Executor : IExecutor
    {
        public IExecutionTarget Target { get; private set; }

        public IKernel Kernel { get; private set; }

        public Executor(IKernel kernel, IExecutionTarget target)
        {
            Kernel = kernel;
            Target = target;

            RegisterDefaultTypes();
            RegisterExecutionTargetTypes();
            StartExecutionTarget();
        }

        protected void RegisterDefaultTypes()
        {
            Kernel.Bind<Level>().To<Level>();
        }

        protected void RegisterExecutionTargetTypes()
        {
            Target.RegisterTypes();
        }

        protected void StartExecutionTarget()
        {
            Target.Start();
        }

        public void Run()
        {
            float dt = 0.01f;

            while (true)
            {
                Step(dt);
            }
        }

        public void Step(float dt)
        {
            if (Target == null)
            {
                return;
            }

            Target.Update(dt);
            Target.Perform();
        }
    }
}

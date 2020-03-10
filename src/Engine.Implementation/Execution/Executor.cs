using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Execution;
using Falcon.Engine.Networking;
using Ninject;

namespace Falcon.Engine.Implementation.Execution
{
    public class Executor : IExecutor
    {
        public IExecutionTarget Target { get; private set; }

        public IKernel Kernel { get; private set; }

        public Executor(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        public IExecutor Init()
        {
            Kernel.Get<IComponentFactory>()
                .Init(Kernel.Get<INotificationHub>());

            Target = Kernel.Get<IExecutionTarget>();
            Target.Init(
                Kernel.Get<INotificationHub>(),
                Kernel.Get<IComponentFactory>(),
                Kernel.Get<IComponentResolverFactory>(),
                Kernel.Get<IStateManager>());

            return this;
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

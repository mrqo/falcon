using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;

namespace Falcon.Engine.Execution
{
    public class Executor
    {
        public List<string> Assemblies { get; }

        public INotificationHub NotificationHub { get; private set; }

        public IComponentFactory ComponentFactory { get; private set; }

        public IComponentResolverFactory ComponentResolverFactory { get; private set; }

        public IExecutionTarget Target { get; private set; }

        public Executor()
        {
            Assemblies = new List<string>();
        }

        public Executor AddAssembly(string assembly)
        {
            Assemblies.Add(assembly);
            return this;
        }

        public Executor SetNotificationHub(INotificationHub hub)
        {
            NotificationHub = hub;
            return this;
        }

        public Executor SetComponentFactory(IComponentFactory factory)
        {
            ComponentFactory = factory;
            return this;
        }

        public Executor SetComponentResolverFactory(IComponentResolverFactory factory)
        {
            ComponentResolverFactory = factory;
            return this;
        }

        public Executor SetTarget(IExecutionTarget target)
        {
            Target = target;
            return this;
        }

        public Executor Init()
        {
            ComponentFactory.Init(NotificationHub);

            Target.Init(
                NotificationHub, 
                ComponentFactory,
                ComponentResolverFactory);

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

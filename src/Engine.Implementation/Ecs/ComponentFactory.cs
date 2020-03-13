using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Implementation.Ecs
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly INotificationHub _notificationHub;

        public ComponentFactory(INotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }
        
        public Component Create(string componentName)
        {
            var type = Type.GetType(componentName);
            return type == null 
                ? null 
                : InitComponent((Component)Activator.CreateInstance(type));
        }

        public TComponent Create<TComponent>() where TComponent : Component =>
            InitComponent((TComponent)Activator.CreateInstance(typeof(TComponent)));

        private TComponent InitComponent<TComponent>(TComponent comp) where TComponent : Component
        {
            comp.RegisterSubscriptions(_notificationHub);
            return comp;
        }
    }
}

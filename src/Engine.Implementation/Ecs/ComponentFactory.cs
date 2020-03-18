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
        
        public Component Create(Type componentType, Entity entity) =>
            InitComponent((Component)Activator.CreateInstance(componentType, entity));

        public TComponent Create<TComponent>(Entity entity) where TComponent : Component =>
            Create(typeof(TComponent), entity) as TComponent;
        
        private TComponent InitComponent<TComponent>(TComponent comp) where TComponent : Component
        {
            comp?.RegisterSubscriptions(_notificationHub);
            return comp;
        }
    }
}

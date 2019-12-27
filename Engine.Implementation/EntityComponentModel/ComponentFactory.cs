using System;
using System.Collections.Generic;
using System.Text;
using Engine.Communication;
using Engine.EntityComponentModel;

namespace Engine.Implementation.EntityComponentModel
{
    public class ComponentFactory : IComponentFactory
    {
        protected INotificationHub NotificationHub { get; private set; }

        public virtual void Init(INotificationHub notificationHub)
        {
            NotificationHub = notificationHub;
        }

        public Component Create(string componentName)
        {
            var type = Type.GetType(componentName);
            if (type == null)
            {
                return null;
            }

            return InitComponent((Component)Activator.CreateInstance(type));
        }

        public TComponent Create<TComponent>() where TComponent : Component
        {
            return InitComponent((TComponent)Activator.CreateInstance(typeof(TComponent)));
        }

        protected TComponent InitComponent<TComponent>(TComponent comp) where TComponent : Component
        {
            comp.RegisterSubscriptions(NotificationHub);

            return comp;
        }
    }
}

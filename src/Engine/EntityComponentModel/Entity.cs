using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Communication;

namespace Falcon.Engine.EntityComponentModel
{
    public abstract class Entity 
        : IComponentResolver
        , IMessageDispatcher
        , INotificationDispatcher
    {
        protected INotificationHub NotificationHub { get; private set; }

        protected IComponentResolver ComponentResolver { get; private set; }

        public IReadOnlyCollection<Component> Components => ComponentResolver.Components;

        public virtual void Init(
            INotificationHub notificationHub,
            IComponentResolver componentResolver)
        {
            NotificationHub = notificationHub;
            ComponentResolver = componentResolver;
        }

        public void AddComponent(Component component)
        {
            component.Entity = this;
            ComponentResolver.AddComponent(component);
        }

        public TComponent FindComponent<TComponent>() where TComponent : Component
        {
            return ComponentResolver.FindComponent<TComponent>();
        }

        public void Update(float dt)
        {
            foreach (var comp in ComponentResolver.Components)
            {
                comp.Dt = dt;
                comp.Update();
            }
        }

        public void DispatchMsg(object msg, object sender)
        {
            foreach (var comp in ComponentResolver.Components)
            {
                comp.ReceiveMsg(msg, sender);
            }
        }

        public void DispatchMsg<TComponent>(object msg, object sender)
        {
            var comps = ComponentResolver.Components
                .Where(comp => comp.GetType() == typeof(TComponent))
                .ToList();

            foreach (var comp in comps)
            {
                comp.ReceiveMsg(msg, sender);
            }
        }

        public bool DispatchNotification(string topic, object msg, object sender)
        {
            return NotificationHub.DispatchNotification(topic, msg, sender);
        }
    }
}

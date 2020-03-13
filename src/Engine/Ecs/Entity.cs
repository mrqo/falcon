using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Communication;

namespace Falcon.Engine.Ecs
{
    public abstract class Entity 
        : IComponentResolver
        , IMessageDispatcher
        , INotificationDispatcher
    {
        protected INotificationHub NotificationHub { get; private set; }

        protected IComponentResolver ComponentResolver { get; private set; }

        public IReadOnlyCollection<Component> Components => ComponentResolver.Components;

        public Entity(
            INotificationHub notificationHub,
            IComponentResolver componentResolver)
        {
            NotificationHub = notificationHub;
            ComponentResolver = componentResolver;
        }

        public IComponentResolver With(Component component)
        {
            component.Entity = this;
            ComponentResolver.With(component);

            return this;
        }

        public Component FindComponent(Type t) =>
            ComponentResolver.FindComponent(t);

        public TComponent FindComponent<TComponent>() where TComponent : Component =>
            ComponentResolver.FindComponent<TComponent>();

        public void Update(float dt)
        {
            foreach (var comp in ComponentResolver.Components)
            {
                comp.Dt = dt;
                comp.Update();
            }
        }

        public void DispatchMsg(object msg, object sender) =>
            ComponentResolver.Components
                .ToList()
                .ForEach(comp => comp.ReceiveMsg(msg, sender));

        public void DispatchMsg<TComponent>(object msg, object sender) =>
            ComponentResolver.Components
                .Where(comp => comp.GetType() == typeof(TComponent))
                .ToList()
                .ForEach(comp => comp.ReceiveMsg(msg, sender));

        public bool DispatchNotification(string topic, object msg, object sender) =>
            NotificationHub.DispatchNotification(topic, msg, sender);
    }
}

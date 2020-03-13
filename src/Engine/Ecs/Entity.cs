using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Engine.Communication;

namespace Falcon.Engine.Ecs
{
    public abstract class Entity 
        : IMessageDispatcher
        , INotificationDispatcher
    {
        private INotificationHub _notificationHub;

        public IComponentResolver ComponentResolver { get; }

        public IReadOnlyCollection<Component> Components => ComponentResolver.Components;

        public Entity(EntityDeps deps)
        {
            _notificationHub = deps.NotificationHub;
            ComponentResolver = deps.ComponentResolver;
            ComponentResolver.Entity = this;
        }

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
            _notificationHub.DispatchNotification(topic, msg, sender);
    }
}

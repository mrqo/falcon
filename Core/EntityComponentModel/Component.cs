using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Engine.Communication;

namespace Engine.EntityComponentModel
{
    public abstract class Component 
        : IMessageBroadcaster
        , IMessageReceiver
        , INotificationBroadcaster
        , INotificationSubscriber
    {
        public Entity Entity { get; set; }

        public float Dt { get; set; }

        public abstract void Update();

        public void Msg(object msg)
        {
            Entity?.DispatchMsg(msg, this);
        }

        public void Msg<TComponent>(object msg)
        {
            Entity?.DispatchMsg<TComponent>(msg, this);
        }

        public virtual void ReceiveMsg(object msg, object sender)
        {
        }

        public virtual void Notify(string topic, object msg)
        {
            Entity?.DispatchNotification(topic, msg, this);
        }

        public virtual void RegisterSubscriptions(INotificationHub hub)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Communication
{
    public interface INotificationHub : INotificationDispatcher
    {
        void Subscribe(string topic, Notification callback);

        bool Unsubscribe(string topic, Notification callback);
    }
}

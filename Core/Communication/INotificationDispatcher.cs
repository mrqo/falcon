using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Communication
{
    public interface INotificationDispatcher
    {
        bool DispatchNotification(string topic, object msg, object sender);
    }
}

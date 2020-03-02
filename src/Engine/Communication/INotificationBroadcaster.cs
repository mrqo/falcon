using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Communication
{
    public interface INotificationBroadcaster
    {
        void Notify(string topic, object msg);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Communication
{
    public interface INotificationSubscriber
    {
        void RegisterSubscriptions(INotificationHub hub);
    }
}

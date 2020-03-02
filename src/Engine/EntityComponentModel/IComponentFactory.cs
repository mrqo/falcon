using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;

namespace Falcon.Engine.EntityComponentModel
{
    public interface IComponentFactory
    {
        void Init(INotificationHub notificationHub);

        Component Create(string componentName);

        TComponent Create<TComponent>() where TComponent : Component;
    }
}

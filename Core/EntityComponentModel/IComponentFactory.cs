using System;
using System.Collections.Generic;
using System.Text;
using Engine.Communication;

namespace Engine.EntityComponentModel
{
    public interface IComponentFactory
    {
        void Init(INotificationHub notificationHub);

        Component Create(string componentName);

        TComponent Create<TComponent>() where TComponent : Component;
    }
}

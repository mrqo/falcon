using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Engine.Communication;
using Engine.EntityComponentModel;

namespace Engine.Execution
{
    public interface IExecutionTarget
    {
        void Init(
            INotificationHub notificationHub, 
            IComponentFactory componentFactory,
            IComponentResolverFactory componentResolverFactory);

        void Update(float dt);

        void Perform();
    }
}

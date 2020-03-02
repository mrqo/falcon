using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;

namespace Falcon.Engine.Execution
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

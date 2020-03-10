using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Networking;

namespace Falcon.Engine.Execution
{
    public interface IExecutionTarget
    {
        void Update(float dt);

        void Perform();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Networking;

namespace Falcon.Engine.Execution
{
    public interface IExecutor
    {
        IExecutor Init();
        void Run();
        void Step(float dt);
    }
}

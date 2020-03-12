using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;
using Falcon.Engine.Networking;

namespace Falcon.Engine.Execution
{
    public interface IExecutor
    {
        void Run();

        void Step(float dt);
    }
}

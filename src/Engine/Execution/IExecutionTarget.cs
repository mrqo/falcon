using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Falcon.Engine.Communication;
using Falcon.Engine.Ecs;
using Falcon.Engine.Networking;

namespace Falcon.Engine.Execution
{
    public interface IExecutionTarget
    {
        void RegisterTypes();

        void Start();

        void Step(float dt);
    }
}

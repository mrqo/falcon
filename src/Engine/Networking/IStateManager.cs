using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Falcon.Engine.Ecs;

namespace Falcon.Engine.Networking
{
    public interface IStateManager
    {
        Task<object> Update(Entity entity);
    }
}

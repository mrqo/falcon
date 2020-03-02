using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Communication
{
    public interface IMessageDispatcher
    {
        void DispatchMsg(object msg, object sender);

        void DispatchMsg<TComponent>(object msg, object sender);
    }
}

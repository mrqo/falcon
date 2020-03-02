using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Communication
{
    public interface IMessageBroadcaster
    {
        void Msg(object msg);

        void Msg<TComponent>(object msg);
    }
}

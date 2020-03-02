using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Engine.Communication
{
    public interface IMessageReceiver
    {
        void ReceiveMsg(object msg, object sender);
    }
}

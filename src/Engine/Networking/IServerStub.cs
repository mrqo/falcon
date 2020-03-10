using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Server;

namespace Falcon.Engine.Networking
{
    public interface IServerStub
    {
        Session.SessionClient SessionClient { get; }
    }
}

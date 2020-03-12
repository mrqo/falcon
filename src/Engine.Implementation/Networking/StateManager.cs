using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Falcon.Engine.Ecs;
using Falcon.Engine.Networking;
using Falcon.Server;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Falcon.Engine.Implementation.Networking
{
    public class StateManager : IStateManager
    {
        private Session.SessionClient _sessionClient;

        public StateManager(IServerStub server)
        {
            _sessionClient = server.SessionClient;
        }

        public async Task<object> Update(Entity entity)
        {
            var createSessionReply = _sessionClient.CreateSession(new CreateSessionRequest());
            if (createSessionReply.SessionId > 0)
            {
                var con = _sessionClient.Connect(new ConnectRequest
                {
                    ClientId = 1,
                    SessionId = createSessionReply.SessionId
                });

                return con.Connected;
            }

            return null;
        }
    }
}

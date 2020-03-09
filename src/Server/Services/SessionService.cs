using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Falcon.Server
{
    public class SessionService : Session.SessionBase
    {
        private readonly ILogger<SessionService> _logger;
        
        // Temporary solution
        public static Dictionary<int, Models.Session> Sessions { get; private set; } =
            new Dictionary<int, Models.Session>();

        public SessionService(ILogger<SessionService> logger)
        {
            _logger = logger;
        }

        public override Task<CreateSessionReply> CreateSession(CreateSessionRequest request, ServerCallContext context)
        {
            var session = Models.Session.Create();

            Sessions.Add(session.SessionId, session);
            _logger.Log(LogLevel.Information, $"Created session {session.SessionId}");

            var reply = new CreateSessionReply
            {
                SessionId = session.SessionId
            };

            return Task.FromResult(reply);
        }

        public override Task<ConnectReply> Connect(ConnectRequest request, ServerCallContext context)
        {
            var session = Sessions.FirstOrDefault(kv => kv.Key == request.SessionId).Value;

            var reply = new ConnectReply
            {
                SessionId = request.SessionId,
                Connected = false
            };

            if (session == null)
            {
                reply.Msg = "Session not found.";
                return Task.FromResult(reply);
            }

            var isInSession = session.Clients.Contains(request.ClientId);
            if (isInSession)
            {
                reply.Msg = "Client already in session.";
                return Task.FromResult(reply);
            }

            session.Clients.Add(request.ClientId);
            reply.Connected = true;
            
            return Task.FromResult(reply);
        }

        public override Task<DisconnectReply> Disconnect(DisconnectRequest request, ServerCallContext context)
        {
            var session = Sessions.FirstOrDefault(kv => kv.Key == request.SessionId).Value;

            var reply = new DisconnectReply
            {
                SessionId = request.SessionId,
                Disconnected = false
            };

            if (session == null)
            {
                reply.Msg = "Session not found.";
                return Task.FromResult(reply);
            }

            var isInSession = session.Clients.Contains(request.ClientId);
            if (!isInSession)
            {
                reply.Msg = "Client doesn't exist in session.";
                return Task.FromResult(reply);
            }

            session.Clients.Remove(request.ClientId);
            reply.Disconnected = true;

            return Task.FromResult(reply);
        }

        public override Task IsConnected(ConnectRequest request, IServerStreamWriter<ConnectReply> responseStream, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override Task<ListClientsReply> ListClients(ConnectRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}

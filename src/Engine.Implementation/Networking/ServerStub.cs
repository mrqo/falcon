using System;
using System.Collections.Generic;
using System.Text;
using Falcon.Engine.Networking;
using Falcon.Server;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace Falcon.Engine.Implementation.Networking
{
    public class ServerStub : IServerStub
    {
        public Session.SessionClient SessionClient { get; }

        public ServerStub()
        {
            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Debug);
            });

            var addr = "https://localhost:5001/";
            var channel = GrpcChannel.ForAddress(addr, new GrpcChannelOptions
            {
                LoggerFactory = loggerFactory
            });

            SessionClient = new Session.SessionClient(channel);
        }
    }
}

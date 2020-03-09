using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Networking;
using Falcon.Server;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Falcon.Engine.Implementation.Networking
{
    public class StateManager : IStateManager
    {
        private Greeter.GreeterClient _greeterClient;

        public StateManager()
        {
            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Debug);
            });

            var channel = GrpcChannel.ForAddress("https://localhost:5001/", new GrpcChannelOptions
            {
                LoggerFactory = loggerFactory
            });

            _greeterClient = new Greeter.GreeterClient(channel);
        }

        public async Task<object> Update(Entity entity)
        {
            var reply = await _greeterClient.SayHelloAsync(new HelloRequest {Name = "Client"});
            return reply.Message;
        }
    }
}

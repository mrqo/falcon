using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Falcon.Engine.EntityComponentModel;
using Falcon.Engine.Networking;
using Falcon.Server;


namespace Falcon.Engine.Implementation.Networking
{
    public class StateManager : IStateManager
    {
        private Greeter.GreeterClient _greeterClient;

        public StateManager()
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _greeterClient = new Greeter.GreeterClient(channel);
        }

        public async Task<object> Update(Entity entity)
        {
            var reply = await _greeterClient.SayHelloAsync(new HelloRequest {Name = "Client"});
            return reply.Message;
        }
    }
}

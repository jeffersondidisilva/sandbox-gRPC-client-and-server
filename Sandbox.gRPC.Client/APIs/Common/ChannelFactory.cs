using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sandbox.gRPC.Server;

namespace Sandbox.gRPC.Client.APIs.Common
{
    public class ChannelFactory
    {
        private const string Address = "https://localhost:5001";
        private static string _token = string.Empty;

        public static GrpcChannel Create()
        {
            Authenticate();
            
            var credentials = CallCredentials.FromInterceptor((_, metadata) =>
            {
                metadata.Add("Authorization", $"Bearer {_token}");
                return Task.CompletedTask;
            });

            var channel = GrpcChannel.ForAddress(Address, new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(),credentials)
            });
            
            return channel;
        }

        private static void Authenticate()
        {
            var channel = GrpcChannel.ForAddress(Address);
            var loginService = new LoginService.LoginServiceClient(channel);
            var result = loginService.Login(new LoginCommand
            {
                Email = "grpc@gmail.com",
                Password = "1234"
            });

            _token = result.Token;
        }
    }
}
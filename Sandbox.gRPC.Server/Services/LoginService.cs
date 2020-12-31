using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Sandbox.gRPC.Server.Helpers;

namespace Sandbox.gRPC.Server.Services
{
    public class LoginService: Server.LoginService.LoginServiceBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IConfiguration _configuration;

        public LoginService(
            JwtHelper jwtHelper,
            IConfiguration configuration
        )
        {
            _jwtHelper = jwtHelper;
            _configuration = configuration;
        }
        
        public override Task<LoginResult> Login(LoginCommand request, ServerCallContext context)
        {
            var email = _configuration["User:Email"];
            var password = _configuration["User:Password"];
            
            if (request.Email == email && request.Password == password)
                return Task.FromResult(_jwtHelper.GenerateToken(request.Email));
            
            return Task.FromResult(new LoginResult
            {
                Success = false
            });
        }
    }
}
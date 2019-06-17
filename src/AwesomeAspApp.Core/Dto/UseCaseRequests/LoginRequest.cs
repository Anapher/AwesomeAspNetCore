using AwesomeAspApp.Core.Dto.UseCaseResponses;
using AwesomeAspApp.Core.Interfaces;

namespace AwesomeAspApp.Core.Dto.UseCaseRequests
{
    public class LoginRequest : IUseCaseRequest<LoginResponse>
    {
        public string Username { get; }
        public string Password { get; }
        public string RemoteIpAddress { get; }

        public LoginRequest(string userName, string password, string remoteIpAddress)
        {
            Username = userName;
            Password = password;
            RemoteIpAddress = remoteIpAddress;
        }
    }
}

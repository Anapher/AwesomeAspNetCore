
using System.Threading.Tasks;
using AwesomeAspApp.Core.Dto;

namespace AwesomeAspApp.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}
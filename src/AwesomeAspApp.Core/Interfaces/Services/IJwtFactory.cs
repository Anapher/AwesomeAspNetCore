
using System.Threading.Tasks;
using AwesomeAspApp.Core.Dto;

namespace AwesomeAspApp.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string id, string userName);
    }
}
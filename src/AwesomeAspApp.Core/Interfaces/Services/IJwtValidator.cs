using System.Security.Claims;

namespace AwesomeAspApp.Core.Interfaces.Services
{
    public interface IJwtValidator
    {
        ClaimsPrincipal? GetPrincipalFromToken(string token);
    }
}
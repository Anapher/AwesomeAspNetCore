using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AwesomeAspApp.Hubs
{
    [Authorize]
    public class CoreHub : Hub
    {
    }
}

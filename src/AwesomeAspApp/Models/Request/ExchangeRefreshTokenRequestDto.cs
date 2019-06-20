#pragma warning disable CS8618 // Non-nullable field is uninitialized. Validators gurantee that.

namespace AwesomeAspApp.Models.Request
{
    public class ExchangeRefreshTokenRequestDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

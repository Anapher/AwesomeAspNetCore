using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AwesomeAspApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController
    {
        // POST api/v1/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Models.Request.LoginRequest request)
        {
            await _loginUseCase.Handle(new LoginRequest(request.UserName, request.Password, Request.HttpContext.Connection.RemoteIpAddress?.ToString()), _loginPresenter);
            return _loginPresenter.ContentResult;
        }

        // POST api/v1/auth/refreshtoken
        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] Models.Request.ExchangeRefreshTokenRequest request)
        {
            await _exchangeRefreshTokenUseCase.Handle(new ExchangeRefreshTokenRequest(request.AccessToken, request.RefreshToken, _authSettings.SecretKey), _exchangeRefreshTokenPresenter);
            return _exchangeRefreshTokenPresenter.ContentResult;
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGKAPI.Application.Features;
using YGKAPI.Application.Features.Commands.User.LoginUser;
using YGKAPI.Application.Features.Commands.User.LoginWithGoogle;
using YGKAPI.Application.Features.Commands.User.RefreshTokenLogin;
using YGKAPI.Application.Features.Queries.User.IsAdminUser;

namespace YGKAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            var response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> IsAdminUser(IsAdminUserQueryRequest isAdminUserQueryRequest)
        {
            var response = await _mediator.Send(isAdminUserQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithGoogle(LoginWithGoogleCommandRequest loginWithGoogleCommandRequest)
        {
            var response = await _mediator.Send(loginWithGoogleCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public IActionResult Status()
        {
            return Ok(new BaseResponse<string>() { });
        }
    }
}

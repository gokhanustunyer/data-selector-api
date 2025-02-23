using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.DTOs.Auth;

namespace YGKAPI.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, BaseResponse<LoginUserCommandResponse>>
    {
        readonly IAuthService _authService;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(IAuthService authService, ILogger<LoginUserCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<BaseResponse<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User login request");
            Token responseToken = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 2700);
            return new ()
            {
                Data = new LoginUserCommandResponse()
                {
                    AccessToken = responseToken.AccessToken,
                    Expiration = responseToken.Expiration,
                    RefreshToken = responseToken.RefreshToken
                }
            };
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ExternalLogins;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Features.Commands.User.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommandRequest, BaseResponse<LoginWithGoogleCommandResponse>>
    {
        private readonly IExternalLoginService _externalLoginService;
        private readonly SignInManager<AppUser> _signInManager;
        public LoginWithGoogleCommandHandler(IExternalLoginService externalLoginService, 
                                             SignInManager<AppUser> signInManager)
        {
            _externalLoginService = externalLoginService;
            _signInManager = signInManager;
        }

        public async Task<BaseResponse<LoginWithGoogleCommandResponse>> Handle(LoginWithGoogleCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _externalLoginService.GoogleLoginAsync(request.IdToken, 900);
            return new()
            {
                Data = new()
                {
                    Token = token
                }
            };
        }
    }
}
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ExternalLogins;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.Abstractions.Token;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Infrastructure.Services.ExternalLogins
{
    public class ExternalLoginService : IExternalLoginService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        public ExternalLoginService(UserManager<AppUser> userManager,
                                    IConfiguration configuration,
                                    ITokenHandler tokenHandler,
                                    IUserService userService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Application.DTOs.Auth.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLogins:Google:ClientID"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload, info, accessTokenLifeTime);
        }

        private async Task<Application.DTOs.Auth.Token> CreateUserExternalAsync(AppUser user, GoogleJsonWebSignature.Payload payload, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = payload.GivenName,
                        Surname = payload.FamilyName,
                        UserName = payload.Email,
                        Email = payload.Email,
                        EmailConfirmed = true,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    };

                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Application.DTOs.Auth.Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new Exception("Invalid external authentication.");
        }
    }
}
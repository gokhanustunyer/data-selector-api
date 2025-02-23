using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.Abstractions.Token;
using YGKAPI.Application.DTOs.Auth;
using YGKAPI.Application.Exceptions.User.Auth;
using YGKAPI.Persistence.Contexts.MySQL;
using YGKAPI.Domain.Entities.Identity;
using YGKAPI.Application.Exceptions.User.Auth.Email;

namespace YGKAPI.Persistence.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHandler _tokenHandler;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _singInManager;
        private readonly IUserService _userService;
        private readonly YGKAPIMySQLDbContext _context;
        public AuthService(UserManager<AppUser> userManager,
                           ITokenHandler tokenHandler,
                           SignInManager<AppUser> singInManager,
                           IUserService userService,
                           YGKAPIMySQLDbContext context)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _singInManager = singInManager;
            _userService = userService;
            _context = context;
        }

        public async Task<bool> IsAdminTokenAsync(string? refreshToken)
        {
            if (refreshToken != null)
            {
                AppUser user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
                if (user != null)
                {
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    bool isSuperAdmin = await _userManager.IsInRoleAsync(user, "Super Admin");
                    return isAdmin || isSuperAdmin;
                }
            }
            return false;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                throw new NotFoundUserException("user.auth.notFoundUserException.message");
            if (!user.EmailConfirmed)
                throw new EmailConfirmException("user.auth.email.emailConfirmException.message");

            SignInResult result = await _singInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 900);
                return token;
            }
            throw new AuthenticationErrorException("user.auth.authenticationErrorException.message");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(2700, user);
                await _userService.UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 900);
                return token;
            }
            else
                throw new NotFoundUserException("user.auth.notFoundUserException.message");
        }
    }
}

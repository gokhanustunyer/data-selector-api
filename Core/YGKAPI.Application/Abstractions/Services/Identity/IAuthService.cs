using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Abstractions.Services.Identity
{
    public interface IAuthService
    {
        Task<DTOs.Auth.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<DTOs.Auth.Token> RefreshTokenLoginAsync(string refreshToken);
        Task<bool> IsAdminTokenAsync(string? token);
    }
}

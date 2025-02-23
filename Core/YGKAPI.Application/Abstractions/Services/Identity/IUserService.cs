using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.DTOs.User;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Abstractions.Services.Identity
{
    public interface IUserService
    {
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionForEndpointAsync(string userName, string code);
        Task<bool> UpdateRefreshToken(AppUser user, string refreshToken, DateTime acccessTokenDate, int addOnAccessTokenSecond);
        Task<bool> CreateAsync(CreateUser model);
        Task<AppUser> GetUserByRefreshTokenAsync(string refreshToken);
        Task<bool> AssignRoleToUser(string userId, string[] roles);
        Task<List<UserList>> GetAllUsersAsync(int page, int size);
        Task<AppUser> GetByUsernameAsync(string username);
        Task UpdateRefreshTokenAsync(string refrestToken, AppUser user, DateTime accressTokenDate, int addOnAccessTokenDate);
        int TotalUserCounts { get; }
    }
}

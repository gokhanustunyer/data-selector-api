using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using YGKAPI.Application.Abstractions.Services.Email;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.DTOs.User;
using YGKAPI.Application.Exceptions.Email;
using YGKAPI.Application.Exceptions.User.Auth;
using YGKAPI.Application.Exceptions.User.Create;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Domain.Entities.Auth;
using YGKAPI.Domain.Entities.Identity;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEndpointRepository _endpointRepository;
        private readonly YGKAPIMySQLDbContext _context;
        private readonly IEmailService _emailService;
        public UserService(UserManager<AppUser> userManager,
                           IEndpointRepository endpointRepository,
                           YGKAPIMySQLDbContext context,
                           IEmailService emailService,
                           RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _endpointRepository = endpointRepository;
            _context = context;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public async Task<bool> HasRolePermissionForEndpointAsync(string userName, string code)
        {
            var userRoles = await GetRolesToUserAsync(userName);
            if (!userRoles.Any()) return false;

            if (userRoles.Contains("Super Admin")) return true;

            Domain.Entities.Auth.Endpoint? endpoint = await _endpointRepository.Table.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code);
            if (endpoint == null) return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;

            return false;
        }
        public async Task<bool> UpdateRefreshToken(AppUser user, string refreshToken, DateTime acccessTokenDate, int addOnAccessTokenSecond)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = acccessTokenDate.AddSeconds(addOnAccessTokenSecond);
                await _userManager.UpdateAsync(user);
                return true;
            }
            throw new NotFoundUserException("user.auth.notFoundUserException.message");
        }
        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            var user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null) user = await _userManager.FindByNameAsync(userIdOrName);
            return (await _userManager.GetRolesAsync(user)).ToArray();
        }

        public async Task<AppUser> GetUserByRefreshTokenAsync(string refreshToken)
        {
            AppUser user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null)
            {
                return user;
            }
            throw new NotFoundUserException("user.auth.notFoundUserException.message");
        }

        public async Task<bool> CreateAsync(CreateUser model)
        {
            if (model.PasswordConfirm != model.Password)
                throw new PasswordMatchException("user.create.passwordMatchException.message");

            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName,
                Email = model.Email,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string erorrMessage = String.Join(' ', result.Errors.Select(e => e.Description));
                throw new UserCreateException("user.create.userCreateException.message");
            }

            var role = await _roleManager.FindByNameAsync("User");
            if (role == null)
            {
                role = new AppRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User"
                };
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, role.Name);

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = $"/confirmEmail?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
            await _emailService.SendConfirmAccountEmailAsync(user.Email, "Havalı hoşgeldin mesajı", url);

            return result.Succeeded;
        }
        public async Task<bool> AssignRoleToUser(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var activeRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, activeRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }
            return true;
        }
        public async Task<List<UserList>> GetAllUsersAsync(int page, int size)
        {
            return _userManager.Users.Skip(page * size).Take(size).Select(u => new UserList()
            {
                Id = u.Id.ToString(),
                Name = u.Name,
                Surname = u.Surname,
                Username = u.UserName,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                DateCreated = u.DateCreated,
            }).ToList();
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
            => await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException("user.auth.notFoundUserException.message");
        }
        public int TotalUserCounts => _userManager.Users.Count();

    }
}
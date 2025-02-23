using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Persistence.Services.Identity
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
            var role = new AppRole()
            {
                Name = name,
                Id = Guid.NewGuid().ToString(),
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public Dictionary<string, string> GetAllRoles()
            => _roleManager.Roles.ToDictionary(role => role.Id, role => role.Name);

        public async Task<(string, string)> GetRoleById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return (role.Id, role.Name);
        }

        public async Task<bool> UpdateRole(string roleId, string name)
        {
            var role = await _roleManager.FindByIdAsync(name);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
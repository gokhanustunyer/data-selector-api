using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Abstractions.Services.Identity
{
    public interface IRoleService
    {
        Dictionary<string, string> GetAllRoles();
        Task<(string id, string name)> GetRoleById(string roleId);
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(string name);
        Task<bool> UpdateRole(string roleId, string name);
    }
}

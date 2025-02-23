using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Abstractions.Services.Endpoint
{
    public interface IEndpointService
    {
        Task AssingRoleEndpoint(string[] roleIds, string menu, string code, Type type);
        Task<List<string>> GetRolesToEndpointAsync(string code, string menu);
        Task<List<Domain.Entities.Auth.Endpoint>> GetEndpointsByRoleId(string roleId);
    }
    
}

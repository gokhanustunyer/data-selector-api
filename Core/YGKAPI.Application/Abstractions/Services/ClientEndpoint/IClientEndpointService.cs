using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.DTOs.ClientEndpoint;
using YGKAPI.Domain.Entities.Auth;

namespace YGKAPI.Application.Abstractions.Services.ClientEndpoint
{
    public interface IClientEndpointService
    {
        Task<bool> AssignRoleToEndpointsByRoleId(string roleId, string[] endpointIds);
        Task<bool> CreateClientEndpointMenu(string name, string apiClientId);
        Task<bool> CreateClientEndpoint(string path, string menuId);
        Task<bool> CreateAPIClient(string domain, int? port, string description,ClientHttpScheme scheme = ClientHttpScheme.HTTPS);
        Task<List<ClientEndpointMenuList>> GetClientEndpointMenusByRoleId(string roleId, string apiClientId);
    }
    
}

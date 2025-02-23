
using YGKAPI.Application.DTOs.ClientEndpoint;
using YGKAPI.Domain.Entities.Auth;

namespace YGKAPI.Application.Features.Queries.ClientEndpoint.GetClientEndpointsByRoleId
{
    public class GetClientEndpointsByRoleIdQueryResponse
    {
        public List<ClientEndpointMenuList> ClientEndpointMenus { get; set; }
    }
}

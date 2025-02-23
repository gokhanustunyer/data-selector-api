
using MediatR;

namespace YGKAPI.Application.Features.Queries.ClientEndpoint.GetClientEndpointsByRoleId
{
    public class GetClientEndpointsByRoleIdQueryRequest : IRequest<BaseResponse<GetClientEndpointsByRoleIdQueryResponse>>
    {
        public string APIClientId { get; set; }
        public string RoleId { get; set; }
    }
}

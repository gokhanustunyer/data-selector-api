
using MediatR;

namespace YGKAPI.Application.Features.Queries.Endpoint.GetEndpointsByRoleId
{
    public class GetEndpointsByRoleIdQueryRequest : IRequest<BaseResponse<GetEndpointsByRoleIdQueryResponse>>
    {
        public string roleId { get; set; }
    }
}

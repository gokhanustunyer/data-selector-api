using MediatR;

namespace YGKAPI.Application.Features.Queries.Endpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryRequest : IRequest<BaseResponse<GetRolesToEndpointQueryResponse>>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}
using MediatR;

namespace YGKAPI.Application.Features.Commands.Endpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandRequest : IRequest<BaseResponse<AssignRoleEndpointCommandResponse>>
    {
        public string[] Roles { get; set; }
        public string EndpointCode { get; set; }
        public string Menu { get; set; }
        public Type? Type { get; set; } = null;
    }
}
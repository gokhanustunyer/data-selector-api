
using MediatR;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.AssignRoleToEndpointsByRoleId
{
    public class AssignRoleToEndpointsByRoleIdCommandRequest : IRequest<BaseResponse<AssignRoleToEndpointsByRoleIdCommandResponse>>
    {
        public string RoleId { get; set; }
        public string[] EndpointIds { get; set; }
    }
}

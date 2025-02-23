
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.AssignRoleToEndpointsByRoleId
{
    public class AssignRoleToEndpointsByRoleIdCommandHandler : IRequestHandler<AssignRoleToEndpointsByRoleIdCommandRequest, BaseResponse<AssignRoleToEndpointsByRoleIdCommandResponse>>
    {
        private readonly IClientEndpointService _clientEndpointService;

        public AssignRoleToEndpointsByRoleIdCommandHandler(IClientEndpointService clientEndpointService)
        {
            _clientEndpointService = clientEndpointService;
        }

        public async Task<BaseResponse<AssignRoleToEndpointsByRoleIdCommandResponse>> Handle(AssignRoleToEndpointsByRoleIdCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _clientEndpointService.AssignRoleToEndpointsByRoleId(request.RoleId, request.EndpointIds);
            return new()
            {
                Data = new()
                {
                    Succeeded = result
                }
            };
        }
    }
}

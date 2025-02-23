using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Endpoint;

namespace YGKAPI.Application.Features.Commands.Endpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, BaseResponse<AssignRoleEndpointCommandResponse>>
    {
        private readonly IEndpointService _endPointService;
        public AssignRoleEndpointCommandHandler(IEndpointService endPointService)
        {
            _endPointService = endPointService;
        }
        public async Task<BaseResponse<AssignRoleEndpointCommandResponse>> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await _endPointService.AssingRoleEndpoint(request.Roles, request.Menu, request.EndpointCode, request.Type);
            return new()
            {
                Succeeded = true,
                Code = 200
            };
        }
    }
}
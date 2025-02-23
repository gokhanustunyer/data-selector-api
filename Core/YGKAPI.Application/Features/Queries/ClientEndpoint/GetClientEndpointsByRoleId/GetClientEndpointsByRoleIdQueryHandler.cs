
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Domain.Entities.Auth;

namespace YGKAPI.Application.Features.Queries.ClientEndpoint.GetClientEndpointsByRoleId
{
    public class GetClientEndpointsByRoleIdQueryHandler : IRequestHandler<GetClientEndpointsByRoleIdQueryRequest, BaseResponse<GetClientEndpointsByRoleIdQueryResponse>>
    {
        private readonly IClientEndpointService _clientEndpointService;
        public GetClientEndpointsByRoleIdQueryHandler(IClientEndpointService clientEndpointService)
        {
            _clientEndpointService = clientEndpointService;
        }

        public async Task<BaseResponse<GetClientEndpointsByRoleIdQueryResponse>> Handle(GetClientEndpointsByRoleIdQueryRequest request, CancellationToken cancellationToken)
        {
            var clientEndpointMenus = await _clientEndpointService.GetClientEndpointMenusByRoleId(request.RoleId, request.APIClientId);
            return new()
            {
                Data = new()
                {
                    ClientEndpointMenus = clientEndpointMenus,
                }
            };
        }
    }
}

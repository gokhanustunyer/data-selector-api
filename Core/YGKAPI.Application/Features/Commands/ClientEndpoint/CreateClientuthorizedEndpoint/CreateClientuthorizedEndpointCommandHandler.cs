
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;
using YGKAPI.Application.Repositories.Auth;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.CreateClientuthorizedEndpoint
{
    public class CreateClientuthorizedEndpointCommandHandler : IRequestHandler<CreateClientuthorizedEndpointCommandRequest, BaseResponse<CreateClientuthorizedEndpointCommandResponse>>
    {
        private readonly IClientEndpointService _clientEndpointService;

        public CreateClientuthorizedEndpointCommandHandler(IClientEndpointService clientEndpointService)
        {
            _clientEndpointService = clientEndpointService;
        }

        public async Task<BaseResponse<CreateClientuthorizedEndpointCommandResponse>> Handle(CreateClientuthorizedEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _clientEndpointService.CreateClientEndpoint(request.Path, request.MenuId);

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

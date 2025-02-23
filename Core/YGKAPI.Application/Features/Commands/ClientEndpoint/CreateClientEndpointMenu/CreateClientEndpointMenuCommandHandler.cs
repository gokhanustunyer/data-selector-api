
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;
using YGKAPI.Application.Repositories.Auth;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.CreateClientEndpointMenu
{
    public class CreateClientEndpointMenuCommandHandler : IRequestHandler<CreateClientEndpointMenuCommandRequest, BaseResponse<CreateClientEndpointMenuCommandResponse>>
    {
        private readonly IClientEndpointService _clientEndpointService;

        public CreateClientEndpointMenuCommandHandler(IClientEndpointService clientEndpointService)
        {
            _clientEndpointService = clientEndpointService;
        }

        public async Task<BaseResponse<CreateClientEndpointMenuCommandResponse>> Handle(CreateClientEndpointMenuCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _clientEndpointService.CreateClientEndpointMenu(request.Name, request.APIClientId);
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

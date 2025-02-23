
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.ClientEndpoint;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.CreateAPIClient
{
    public class CreateAPIClientCommandHandler : IRequestHandler<CreateAPIClientCommandRequest, BaseResponse<CreateAPIClientCommandResponse>>
    {
        private readonly IClientEndpointService _clientEndpointService;

        public CreateAPIClientCommandHandler(IClientEndpointService clientEndpointService)
        {
            _clientEndpointService = clientEndpointService;
        }

        public async Task<BaseResponse<CreateAPIClientCommandResponse>> Handle(CreateAPIClientCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _clientEndpointService.CreateAPIClient(request.Domain, request.Port, request.Description, request.Scheme);
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

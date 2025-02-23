using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Endpoint;

namespace YGKAPI.Application.Features.Queries.Endpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, BaseResponse<GetRolesToEndpointQueryResponse>>
    {
        private readonly IEndpointService _endpointService;

        public GetRolesToEndpointQueryHandler(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        public async Task<BaseResponse<GetRolesToEndpointQueryResponse>> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _endpointService.GetRolesToEndpointAsync(request.Code, request.Menu);
            return new()
            {
                Data = new()
                {
                    Roles = datas
                }
            };
        }
    }
}
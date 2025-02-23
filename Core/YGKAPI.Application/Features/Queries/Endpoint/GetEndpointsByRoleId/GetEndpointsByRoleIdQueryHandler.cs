
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Endpoint;

namespace YGKAPI.Application.Features.Queries.Endpoint.GetEndpointsByRoleId
{
    public class GetEndpointsByRoleIdQueryHandler : IRequestHandler<GetEndpointsByRoleIdQueryRequest, BaseResponse<GetEndpointsByRoleIdQueryResponse>>
    {
        private readonly IEndpointService _endpointService;

        public GetEndpointsByRoleIdQueryHandler(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }
        public async Task<BaseResponse<GetEndpointsByRoleIdQueryResponse>> Handle(GetEndpointsByRoleIdQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _endpointService.GetEndpointsByRoleId(request.roleId);
            return new()
            {
                Data = new()
                {
                    Endpoints = datas
                }
            };
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;

namespace YGKAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler: IRequestHandler<GetAllRolesQueryRequest, BaseResponse<GetAllRolesQueryResponse>>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<BaseResponse<GetAllRolesQueryResponse>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = _roleService.GetAllRoles();
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
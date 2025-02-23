using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Features.Queries.User.IsAdminUser
{
    public class IsAdminUserQueryHandler : IRequestHandler<IsAdminUserQueryRequest, BaseResponse<IsAdminUserQueryResponse>>
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public IsAdminUserQueryHandler(IUserService userService,
                                       UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<BaseResponse<IsAdminUserQueryResponse>> Handle(IsAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<IsAdminUserQueryResponse> returnObject = new() { Data = new() { IsAdmin = false } };
            if (request.Username != null)
            {
                AppUser user = await _userService.GetByUsernameAsync(request.Username);
                if (user != null)
                {
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    bool isSuperAdmin = await _userManager.IsInRoleAsync(user, "Super Admin");
                    returnObject.Data.IsAdmin = isAdmin || isSuperAdmin;
                }
            }
            return returnObject;
        }
    }
}
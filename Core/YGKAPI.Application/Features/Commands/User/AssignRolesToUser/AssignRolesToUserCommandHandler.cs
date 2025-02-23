using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;

namespace YGKAPI.Application.Features.Commands.User.AssignRolesToUser
{
    public class AssignRolesToUserCommandHandler : IRequestHandler<AssignRolesToUserCommandRequest, BaseResponse<AssignRolesToUserCommandResponse>>
    {
        private readonly IUserService _userService;

        public AssignRolesToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse<AssignRolesToUserCommandResponse>> Handle(AssignRolesToUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.AssignRoleToUser(request.UserId, request.RoleNames);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
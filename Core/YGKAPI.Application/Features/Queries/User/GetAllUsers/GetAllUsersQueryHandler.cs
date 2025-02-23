using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.DTOs.User;

namespace YGKAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, BaseResponse<GetAllUsersQueryResponse>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            List<UserList> users = await _userService.GetAllUsersAsync(request.Page, request.Size);
            return new()
            {
                Data = new()
                {
                    Users = users,
                    TotalUserCount = _userService.TotalUserCounts
                }
            };
        }
    }
}
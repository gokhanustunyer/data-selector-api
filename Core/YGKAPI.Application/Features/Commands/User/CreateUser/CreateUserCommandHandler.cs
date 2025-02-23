using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Identity;
using YGKAPI.Application.Exceptions.Email;
using YGKAPI.Application.Exceptions.User.Create;

namespace YGKAPI.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, BaseResponse<CreateUserCommandResponse>>
    {
        readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<BaseResponse<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            DTOs.User.CreateUser model = new DTOs.User.CreateUser()
            {
                Email = request.Email,
                UserName = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm
            };

            bool result = await _userService.CreateAsync(model);
            return new()
            {
                Data = new() { Succeeded = result }
            };
        }
    }
}
﻿using MediatR;

namespace YGKAPI.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandRequest : IRequest<BaseResponse<CreateUserCommandResponse>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
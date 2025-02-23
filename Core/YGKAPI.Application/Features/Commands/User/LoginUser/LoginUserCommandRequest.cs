using MediatR;

namespace YGKAPI.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandRequest : IRequest<BaseResponse<LoginUserCommandResponse>>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
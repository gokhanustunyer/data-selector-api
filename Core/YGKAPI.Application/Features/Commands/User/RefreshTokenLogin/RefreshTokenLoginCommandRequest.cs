using MediatR;

namespace YGKAPI.Application.Features.Commands.User.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<BaseResponse<RefreshTokenLoginCommandResponse>>
    {
        public string RefreshToken { get; set; }
    }
}
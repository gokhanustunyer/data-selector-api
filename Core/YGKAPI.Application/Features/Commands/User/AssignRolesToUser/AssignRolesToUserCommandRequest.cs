using MediatR;

namespace YGKAPI.Application.Features.Commands.User.AssignRolesToUser
{
    public class AssignRolesToUserCommandRequest : IRequest<BaseResponse<AssignRolesToUserCommandResponse>>
    {
        public string UserId { get; set; }
        public string[] RoleNames { get; set; }
    }
}
using MediatR;

namespace YGKAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandRequest : IRequest<BaseResponse<DeleteRoleCommandResponse>>
    {
        public string Name { get; set; }
    }
}
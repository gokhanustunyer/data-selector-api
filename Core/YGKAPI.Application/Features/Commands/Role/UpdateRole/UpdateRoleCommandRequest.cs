using MediatR;

namespace YGKAPI.Application.Features.Commands.Role.UpdateRole
{
    public class UpdateRoleCommandRequest : IRequest<BaseResponse<UpdateRoleCommandResponse>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
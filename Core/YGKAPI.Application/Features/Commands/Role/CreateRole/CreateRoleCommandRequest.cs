using MediatR;

namespace YGKAPI.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<BaseResponse<CreateRoleCommandResponse>>
    {
        public string Name { get; set; }
    }
}
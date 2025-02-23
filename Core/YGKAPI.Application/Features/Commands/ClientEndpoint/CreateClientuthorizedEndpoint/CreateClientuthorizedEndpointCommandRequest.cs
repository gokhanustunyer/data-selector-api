
using MediatR;
using YGKAPI.Domain.Entities.Auth;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.CreateClientuthorizedEndpoint
{
    public class CreateClientuthorizedEndpointCommandRequest : IRequest<BaseResponse<CreateClientuthorizedEndpointCommandResponse>>
    {
        public string Path { get; set; }
        public string MenuId { get; set; }
    }
}

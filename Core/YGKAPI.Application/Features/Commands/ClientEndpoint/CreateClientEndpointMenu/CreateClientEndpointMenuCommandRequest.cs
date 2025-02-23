
using MediatR;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.CreateClientEndpointMenu
{
    public class CreateClientEndpointMenuCommandRequest : IRequest<BaseResponse<CreateClientEndpointMenuCommandResponse>>
    {
        public string Name { get; set; }
        public string APIClientId { get; set; }
    }
}

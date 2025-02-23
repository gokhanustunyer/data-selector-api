
using MediatR;
using YGKAPI.Domain.Entities.Auth;

namespace YGKAPI.Application.Features.Commands.ClientEndponint.CreateAPIClient
{
    public class CreateAPIClientCommandRequest : IRequest<BaseResponse<CreateAPIClientCommandResponse>>
    {
        public string Domain { get; set; }
        public int? Port { get; set; }
        public string Description { get; set; }
        public ClientHttpScheme Scheme { get; set; }
    }
}

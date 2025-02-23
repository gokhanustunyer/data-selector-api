
using MediatR;

namespace YGKAPI.Application.Features.Queries.DbCredentials.GetCredentialList
{
    public class GetCredentialListQueryRequest : IRequest<BaseResponse<GetCredentialListQueryResponse>>
    {
        public string? Username { get; set; }
    }
}

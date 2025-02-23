
using YGKAPI.Domain.Entities.DbCredentials;

namespace YGKAPI.Application.Features.Queries.DbCredentials.GetCredentialList
{
    public class GetCredentialListQueryResponse
    {
        public List<Domain.Entities.DbCredentials.DbCredentials> DbCredentials { get; set; }
    }
}

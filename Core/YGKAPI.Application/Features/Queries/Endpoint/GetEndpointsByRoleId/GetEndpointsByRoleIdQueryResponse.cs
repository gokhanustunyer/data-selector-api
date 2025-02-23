
namespace YGKAPI.Application.Features.Queries.Endpoint.GetEndpointsByRoleId
{
    public class GetEndpointsByRoleIdQueryResponse
    {
        public IEnumerable<Domain.Entities.Auth.Endpoint> Endpoints { get; set; }
    }
}

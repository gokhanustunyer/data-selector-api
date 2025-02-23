using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories.Auth
{
    public class ClientEndpointRepository : Repository<Domain.Entities.Auth.ClientEndpoint>, IClientEndpointRepository
    {
        public ClientEndpointRepository(YGKAPIMySQLDbContext context) : base(context)
        {
        }
    }
}

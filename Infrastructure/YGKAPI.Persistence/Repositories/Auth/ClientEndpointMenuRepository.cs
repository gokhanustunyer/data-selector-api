using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories.Auth
{
    public class ClientEndpointMenuRepository : Repository<Domain.Entities.Auth.ClientEndpointMenu>, IClientEndpointMenuRepository
    {
        public ClientEndpointMenuRepository(YGKAPIMySQLDbContext context) : base(context)
        {
        }
    }
}

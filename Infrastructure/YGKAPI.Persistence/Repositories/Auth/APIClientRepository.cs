using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories.Auth
{
    public class APIClientRepository : Repository<Domain.Entities.Auth.APIClient>, IAPIClientRepository
    {
        public APIClientRepository(YGKAPIMySQLDbContext context) : base(context)
        {
        }
    }
}

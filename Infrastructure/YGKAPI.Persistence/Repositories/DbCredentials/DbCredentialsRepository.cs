using YGKAPI.Application.Repositories.DbCredentials;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories.DbCredentials
{
    public class DbCredentialsRepository : Repository<Domain.Entities.DbCredentials.DbCredentials>, IDbCredentialsRepository
    {
        public DbCredentialsRepository(YGKAPIMySQLDbContext context) : base(context)
        {
        }
    }
}

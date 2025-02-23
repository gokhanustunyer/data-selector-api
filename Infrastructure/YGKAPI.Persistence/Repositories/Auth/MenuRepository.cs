using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories.Auth
{
    public class MenuRepository : Repository<Domain.Entities.Auth.Menu>, IMenuRepository
    {
        public MenuRepository(YGKAPIMySQLDbContext context) : base(context)
        {
        }
    }
}
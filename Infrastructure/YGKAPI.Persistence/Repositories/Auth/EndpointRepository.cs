using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Repositories;
using YGKAPI.Application.Repositories.Auth;
using YGKAPI.Persistence.Contexts.MySQL;

namespace YGKAPI.Persistence.Repositories.Auth
{
    public class EndpointRepository : Repository<Domain.Entities.Auth.Endpoint>, IEndpointRepository
    {
        public EndpointRepository(YGKAPIMySQLDbContext context) : base(context)
        {
        }
    }
}
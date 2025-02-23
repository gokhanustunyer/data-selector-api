using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Domain.Entities.Auth
{
    public class ClientEndpoint : BaseEntity
    {
        public ClientEndpoint()
        {
            Roles = new HashSet<AppRole>();
        }
        public string Path { get; set; }
        public ClientEndpointMenu ClientEndpointMenu { get; set; }
        public ICollection<AppRole> Roles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Domain.Entities.Auth
{
    public class APIClient: BaseEntity
    {
        public ClientHttpScheme Scheme { get; set; }
        public string Domain { get; set; }
        public int? Port { get; set; }
        public string Description { get; set; }
        public ICollection<ClientEndpointMenu> ClientMenus { get; set; }
    }
}
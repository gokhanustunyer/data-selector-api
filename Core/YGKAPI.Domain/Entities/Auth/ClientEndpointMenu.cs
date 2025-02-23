using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Domain.Entities.Auth
{
    public class ClientEndpointMenu : BaseEntity
    {
        public string Name { get; set; }
        public APIClient APIClient { get; set; }
        public ICollection<ClientEndpoint> ClientEndpoints { get; set; }
    }
}
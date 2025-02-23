using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.DTOs.ClientEndpoint
{
    public class ClientEndpointMenuList
    {
        public string MenuName { get; set; }
        public List<string> Endpoints { get; set; }
    }
}
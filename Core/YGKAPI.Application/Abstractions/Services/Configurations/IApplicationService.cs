using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities.Auth;

namespace YGKAPI.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<Application.DTOs.Configuration.Menu> GetAuthorizeDefinitionEndPoints(Type type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Auth.Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Abstractions.Services.ExternalLogins
{
    public interface IExternalLoginService
    {
        Task<DTOs.Auth.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}

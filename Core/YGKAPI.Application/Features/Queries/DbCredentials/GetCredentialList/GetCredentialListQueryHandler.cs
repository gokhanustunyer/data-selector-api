
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Repositories.DbCredentials;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Features.Queries.DbCredentials.GetCredentialList
{
    public class GetCredentialListQueryHandler : IRequestHandler<GetCredentialListQueryRequest, BaseResponse<GetCredentialListQueryResponse>>
    {
        private readonly IDbCredentialsRepository _dbCredentialsRepository;
        private readonly UserManager<AppUser> _userManager;
        private static Random random = new Random();

        public GetCredentialListQueryHandler(IDbCredentialsRepository dbCredentialsRepository, 
                                             UserManager<AppUser> userManager)
        {
            _dbCredentialsRepository = dbCredentialsRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse<GetCredentialListQueryResponse>> Handle(GetCredentialListQueryRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByNameAsync(request.Username);
            var credentials = _dbCredentialsRepository.Table.Where(c => c.UserId == user.Id).Select(c => new Domain.Entities.DbCredentials.DbCredentials()
            {
                Id = c.Id,
                Username = c.Username,
                Password = GenerateRandomString(c.Password.Length),
                ConnectionString = c.ConnectionString,
                DateCreated = c.DateCreated,
                DateUpdated = c.DateUpdated,
                DbType = c.DbType,
                Title = c.Title,
            }).ToList();
            return new()
            {
                Data = new()
                {
                    DbCredentials = credentials
                }
            };
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random random = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            return new string(password);
        }
    }
}

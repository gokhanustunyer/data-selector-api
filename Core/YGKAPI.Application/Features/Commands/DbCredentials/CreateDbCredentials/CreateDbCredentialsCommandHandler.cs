
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Repositories.DbCredentials;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Features.Commands.DbCredentials.CreateDbCredentials
{
    public class CreateDbCredentialsCommandHandler : IRequestHandler<CreateDbCredentialsCommandRequest, BaseResponse<CreateDbCredentialsCommandResponse>>
    {
        private readonly IDbCredentialsRepository _dbCredentialsRepository;
        private readonly UserManager<AppUser> _userManager;
        public CreateDbCredentialsCommandHandler(IDbCredentialsRepository dbCredentialsRepository, UserManager<AppUser> userManager)
        {
            _dbCredentialsRepository = dbCredentialsRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse<CreateDbCredentialsCommandResponse>> Handle(CreateDbCredentialsCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByNameAsync(request.loggedUsername);
            await _dbCredentialsRepository.AddAsync(new Domain.Entities.DbCredentials.DbCredentials()
            {
                DbType = request.DbType,
                ConnectionString = request.ConnectionString,
                Password = request.Password,
                Title = request.Title,
                Username = request.Username,
                User = user,
                UserId = user.Id
            });
            await _dbCredentialsRepository.SaveAsync();
            return new()
            {
                Data = new()
                {
                    Succeeded = true,
                }
            };
        }
    }
}

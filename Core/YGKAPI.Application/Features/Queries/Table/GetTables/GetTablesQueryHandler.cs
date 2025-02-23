
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.DbServices;
using YGKAPI.Application.DTOs.Table;
using YGKAPI.Application.Repositories.DbCredentials;
using YGKAPI.Domain.Entities.DbCredentials;
using YGKAPI.Domain.Entities.Identity;

namespace YGKAPI.Application.Features.Queries.Table.GetTables
{
    public class GetTablesQueryHandler : IRequestHandler<GetTablesQueryRequest, BaseResponse<GetTablesQueryResponse>>
    {
        private readonly IDbCredentialsRepository _dbCredentialsRepository;
        private readonly IMySqlService _mySQLService;
        public GetTablesQueryHandler(IDbCredentialsRepository dbCredentialsRepository,
                                     UserManager<AppUser> userManager,
                                     IMySqlService mySQLService)
        {
            _dbCredentialsRepository = dbCredentialsRepository;
            _mySQLService = mySQLService;
        }

        public async Task<BaseResponse<GetTablesQueryResponse>> Handle(GetTablesQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.DbCredentials.DbCredentials credentials = await _dbCredentialsRepository.GetByIdAsync(request.DbCredentialId);
            List<DbTable> tables = await _mySQLService.GetTablesAsync(credentials);
            return new()
            {
                Data = new()
                {
                    Tables = tables
                }
            };
        }
    }
}


using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.DbServices;
using YGKAPI.Application.DTOs.DbTable;
using YGKAPI.Application.Repositories.DbCredentials;
using YGKAPI.Domain.Entities.DbCredentials;

namespace YGKAPI.Application.Features.Queries.Table.GetTableDatasBySqlQuery
{
    public class GetTableDatasBySqlQueryQueryHandler : IRequestHandler<GetTableDatasBySqlQueryQueryRequest, BaseResponse<GetTableDatasBySqlQueryQueryResponse>>
    {
        private readonly IDbCredentialsRepository _dbCredentialsRepository;
        private readonly IMySqlService _mySqlService;

        public GetTableDatasBySqlQueryQueryHandler(IDbCredentialsRepository dbCredentialsRepository,
                                                   IMySqlService mySqlService)
        {
            _dbCredentialsRepository = dbCredentialsRepository;
            _mySqlService = mySqlService;
        }

        public async Task<BaseResponse<GetTableDatasBySqlQueryQueryResponse>> Handle(GetTableDatasBySqlQueryQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.DbCredentials.DbCredentials dbCredentials = await _dbCredentialsRepository.GetByIdAsync(request.DbCredentialsId);
            TableData datas = await _mySqlService.GetTableDatasBySqlQueryAsync(dbCredentials, request.SqlQuery);
            return new()
            {
                Data = new()
                {
                    TableDatas = datas
                }
            };
        }
    }
}

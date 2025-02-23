
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

namespace YGKAPI.Application.Features.Queries.Table.GetTableDatas
{
    public class GetTableDatasQueryHandler : IRequestHandler<GetTableDatasQueryRequest, BaseResponse<GetTableDatasQueryResponse>>
    {
        private readonly IMySqlService _mySqlService;
        private readonly IDbCredentialsRepository _dbCredentialsRepository;

        public GetTableDatasQueryHandler(IMySqlService mySqlService, 
                                         IDbCredentialsRepository dbCredentialsRepository)
        {
            _mySqlService = mySqlService;
            _dbCredentialsRepository = dbCredentialsRepository;
        }

        public async Task<BaseResponse<GetTableDatasQueryResponse>> Handle(GetTableDatasQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.DbCredentials.DbCredentials dbCredentials = await _dbCredentialsRepository.GetByIdAsync(request.DbCredentialsId);
            TableData datas = await _mySqlService.GetTableDatasAsync(dbCredentials, request.TableName);
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

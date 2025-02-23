
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.DbServices;
using YGKAPI.Application.Repositories.DbCredentials;

namespace YGKAPI.Application.Features.Queries.Table.DownloadTable
{
    public class DownloadTableQueryHandler : IRequestHandler<DownloadTableQueryRequest, BaseResponse<DownloadTableQueryResponse>>
    {
        private readonly IMySqlService _mySqlService;
        private readonly IDbCredentialsRepository _dbCredentialsRepository;

        public DownloadTableQueryHandler(IMySqlService mySqlService, 
                                         IDbCredentialsRepository dbCredentialsRepository)
        {
            _mySqlService = mySqlService;
            _dbCredentialsRepository = dbCredentialsRepository;
        }

        public async Task<BaseResponse<DownloadTableQueryResponse>> Handle(DownloadTableQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.DbCredentials.DbCredentials dbCredentials = await _dbCredentialsRepository.GetByIdAsync(request.DbCredentialsId);
            DataTable dataTable = await _mySqlService.GetTableDatasAsDataTableAsync(dbCredentials, request.TableName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cell(1, 1).InsertTable(dataTable);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var fileContent = stream.ToArray();

                    return new()
                    {
                        Data = new()
                        {
                            ResultFile = Convert.ToBase64String(fileContent)
                        }
                    };
                }
            }
        }
    }
}

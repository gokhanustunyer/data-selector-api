
using MediatR;

namespace YGKAPI.Application.Features.Queries.Table.DownloadTable
{
    public class DownloadTableQueryRequest : IRequest<BaseResponse<DownloadTableQueryResponse>>
    {
        public string ExportType { get; set; }
        public string DbCredentialsId { get; set; }
        public string TableName { get; set; }
    }
}

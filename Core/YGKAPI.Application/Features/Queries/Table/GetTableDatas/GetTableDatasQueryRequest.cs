
using MediatR;

namespace YGKAPI.Application.Features.Queries.Table.GetTableDatas
{
    public class GetTableDatasQueryRequest : IRequest<BaseResponse<GetTableDatasQueryResponse>>
    {
        public string DbCredentialsId { get; set; }
        public string TableName { get; set; }
    }
}

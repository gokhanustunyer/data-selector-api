
using MediatR;

namespace YGKAPI.Application.Features.Queries.Table.GetTableDatasBySqlQuery
{
    public class GetTableDatasBySqlQueryQueryRequest : IRequest<BaseResponse<GetTableDatasBySqlQueryQueryResponse>>
    {
        public string DbCredentialsId { get; set; }
        public string SqlQuery { get; set; }
    }
}

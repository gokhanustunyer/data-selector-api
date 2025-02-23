
using MediatR;

namespace YGKAPI.Application.Features.Queries.Table.GetTables
{
    public class GetTablesQueryRequest : IRequest<BaseResponse<GetTablesQueryResponse>>
    {
        public string DbCredentialId { get; set; }
    }
}

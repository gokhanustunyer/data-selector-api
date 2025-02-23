using MediatR;

namespace YGKAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest<BaseResponse<GetRoleByIdQueryResponse>>
    {
        public string Id { get; set; }
    }
}
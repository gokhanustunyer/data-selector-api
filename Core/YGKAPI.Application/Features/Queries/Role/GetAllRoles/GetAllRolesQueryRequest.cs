using MediatR;

namespace YGKAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryRequest : IRequest<BaseResponse<GetAllRolesQueryResponse>>
    {
    }
}
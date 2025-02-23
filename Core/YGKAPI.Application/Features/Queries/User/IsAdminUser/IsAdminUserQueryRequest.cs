using MediatR;

namespace YGKAPI.Application.Features.Queries.User.IsAdminUser
{
    public class IsAdminUserQueryRequest : IRequest<BaseResponse<IsAdminUserQueryResponse>>
    {
        public string Username { get; set; }
    }
}
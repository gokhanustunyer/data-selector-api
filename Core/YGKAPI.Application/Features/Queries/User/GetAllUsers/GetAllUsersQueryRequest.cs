using MediatR;

namespace YGKAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<BaseResponse<GetAllUsersQueryResponse>>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 15;
    }
}
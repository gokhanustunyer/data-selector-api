using YGKAPI.Application.DTOs.User;

namespace YGKAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public IEnumerable<UserList> Users { get; set; }
        public int TotalUserCount { get; set; }
    }
}
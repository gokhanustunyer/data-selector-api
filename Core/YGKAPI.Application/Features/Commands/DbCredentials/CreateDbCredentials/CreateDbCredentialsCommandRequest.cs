
using MediatR;
using YGKAPI.Domain.Entities.DbCredentials;

namespace YGKAPI.Application.Features.Commands.DbCredentials.CreateDbCredentials
{
    public class CreateDbCredentialsCommandRequest : IRequest<BaseResponse<CreateDbCredentialsCommandResponse>>
    {
        public string? loggedUsername { get; set; }
        public string Title { get; set; }
        public string ConnectionString { get; set; }
        public DbType DbType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

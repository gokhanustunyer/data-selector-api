namespace YGKAPI.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
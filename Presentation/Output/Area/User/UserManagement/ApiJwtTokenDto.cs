namespace Presentation.Output.Area.User.UserManagement
{
    public class ApiJwtTokenDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}

namespace Presentation.Output.Area.User.UserManagement
{
    public class ApiGetUserListDto
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
    }
}

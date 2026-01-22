namespace Application.Services.Implement.Queries.Users.UserManagement.GetUserList
{
    public class GetUserListResultDto
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
    }
}

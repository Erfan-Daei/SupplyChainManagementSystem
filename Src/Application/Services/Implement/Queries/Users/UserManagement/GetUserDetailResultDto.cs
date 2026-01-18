namespace Application.Services.Implement.Queries.Users.UserManagement
{
    public class GetUserDetailResultDto
    {
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserCompanyName { get; set; } = null!;
        public Guid UserCompanyId { get; set; }
        public string UserRole { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

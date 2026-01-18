namespace Presentation.Output.Area.User.UserManagement
{
    public class ApiGetUserDetailDto
    {
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserCompanyName { get; set; } = null!;
        public Guid UserCompanyId { get; set; }
        public string UserRole { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

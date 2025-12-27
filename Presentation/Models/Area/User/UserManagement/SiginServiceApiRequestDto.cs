namespace Presentation.Models.Area.User.UserManagement
{
    public class SiginServiceApiRequestDto
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ConPassword { get; set; }
        public Guid CompanyId { get; set; }
    }
}

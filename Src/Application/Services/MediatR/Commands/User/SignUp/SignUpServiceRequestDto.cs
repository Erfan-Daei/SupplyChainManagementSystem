namespace Application.Services.MediatR.Commands.User.SignUp
{
    public class SignUpServiceRequestDto
    {
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public string? ConPassword { get; set; }
        public Guid CompanyId { get; set; }
    }
}

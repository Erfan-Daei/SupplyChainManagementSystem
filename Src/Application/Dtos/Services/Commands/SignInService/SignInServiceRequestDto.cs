namespace Application.Dtos.Services.Commands.SignInService
{
    public class SignInServiceRequestDto
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ConPassword { get; set; }
        public Guid CompanyId { get; set; }
    }
}

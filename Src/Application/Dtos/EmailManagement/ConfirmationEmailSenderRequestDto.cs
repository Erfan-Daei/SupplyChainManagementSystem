namespace Application.Dtos.EmailManagement
{
    //Request class for ConfirmationEmailSenderService
    public class ConfirmationEmailSenderRequestDto
    {
        public string UserEmail { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Value { get; set; } = null!;
        public ConfirmationEmailSenderType EmailSenderType { get; set; }
    }
}

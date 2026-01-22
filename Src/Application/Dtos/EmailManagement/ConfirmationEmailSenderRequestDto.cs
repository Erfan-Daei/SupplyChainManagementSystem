namespace Application.Dtos.EmailManagement
{
    //request class for ConfirmationEmailSender service
    public class ConfirmationEmailSenderRequestDto
    {
        public string UserEmail { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string ActivationLink { get; set; } = null!;
    }
}

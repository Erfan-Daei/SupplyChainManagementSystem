namespace Application.Dtos.EmailManagement
{
    //request class for ConfirmationEmailSender service
    public class ConfirmationEmailSenderRequestDto
    {
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public string Subject { get; set; }
        public string ActivationLink { get; set; }
    }
}

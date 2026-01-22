namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService
{
    public class EditServiceRequestDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
    }
}

namespace Presentation.Output.Area.User.ServiceManagement
{
    public class ApiGetServiceListDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
    }
}

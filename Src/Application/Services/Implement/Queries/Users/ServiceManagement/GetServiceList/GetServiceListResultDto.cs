namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceList
{
    public class GetServiceListResultDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
    }
}

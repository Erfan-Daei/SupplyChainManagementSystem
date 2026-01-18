namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail
{
    public class GetServiceDetailResultDto
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Creator { get; set; }
    }
}

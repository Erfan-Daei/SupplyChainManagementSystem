namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyDetail
{
    public class GetCompanyDetailResultDto
    {
        public string CompanyName { get; set; } = null!;
        public int UserCount { get; set; }
        public int AsSupplierCount { get; set; }
        public int AsConsumerCount { get; set; }
        public List<GetCompanyDetailCompanyServicesDto> CompanyServices { get; set; } = [];
    }
    public class GetCompanyDetailCompanyServicesDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
    }
}

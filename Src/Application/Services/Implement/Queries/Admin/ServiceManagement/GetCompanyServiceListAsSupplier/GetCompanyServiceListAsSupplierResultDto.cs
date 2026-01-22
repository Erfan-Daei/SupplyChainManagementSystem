namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier
{
    public class GetCompanyServiceListAsSupplierResultDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
    }
}

namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail
{
    public class GetServiceDetailResultDto
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
        public bool ServiceIsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Creator { get; set; }
        public List<GetServiceDetailSupplierCompanyDto> SupplierCompanies { get; set; } = [];
        public int SupplyRelationCount { get; set; }
    }

    public class GetServiceDetailSupplierCompanyDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}

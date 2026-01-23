namespace Presentation.Output.Area.User.ServiceManagement
{
    public class ApiGetServiceDetailDro
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
        public bool ServiceIsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Creator { get; set; }
        public List<ApiGetServiceDetailSupplierCompanyDto> SupplierCompanies { get; set; } = [];
        public int SupplyRelationCount { get; set; }
    }

    public class ApiGetServiceDetailSupplierCompanyDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}

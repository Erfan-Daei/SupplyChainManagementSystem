namespace Presentation.Output.Area.Admin.ServiceManagement
{
    public class ApiGetCompanyServiceListAsSupplierDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
    }
}

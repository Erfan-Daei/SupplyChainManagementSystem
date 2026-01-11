namespace Presentation.Output.Area.Admin.ServiceManagement
{
    public class ApiGetCompanyDetaiDto
    {
        public string CompanyName { get; set; } = null!;
        public int UserCount { get; set; }
        public int AsSupplierCount { get; set; }
        public int AsConsumerCount { get; set; }
    }
}

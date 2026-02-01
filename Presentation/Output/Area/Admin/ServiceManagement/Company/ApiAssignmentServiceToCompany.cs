using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Company
{
    public class ApiAssignmentServiceToCompany
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiAssignmentServiceToCompany Result(Guid companyId, Guid serviceId, IUrlHelper url)
        {
            return new ApiAssignmentServiceToCompany
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = companyId})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetServiceDetail", "ServiceManager", new {serviceId = serviceId})!,
                        Method = "GET" ,
                        Rel = "ServiceDetail"
                    },
                }
            };
        }
    }
}

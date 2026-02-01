using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Company
{
    public class ApiEditCompanyResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiEditCompanyResult Result(Guid companyId, IUrlHelper url)
        {
            return new ApiEditCompanyResult
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
                        Href = url.ActionLink("DeleteCompany", "CompanyManager", new {companyId = companyId})!,
                        Method = "DELETE" ,
                        Rel = "Delete"
                    },
                }
            };
        }
    }
}

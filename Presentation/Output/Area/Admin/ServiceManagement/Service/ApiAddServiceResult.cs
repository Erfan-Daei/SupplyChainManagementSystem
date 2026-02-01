using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Area.Admin.ServiceManagement.Company;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Service
{
    public class ApiAddServiceResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiAddCompanyResult Result(Guid? serviceId, IUrlHelper url)
        {
            if (serviceId == null)
                return new ApiAddCompanyResult();

            return new ApiAddCompanyResult
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetServiceDetail", "ServiceManager", new {serviceId = serviceId})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("EditService", "ServiceManager", new {ServiceId = serviceId, ServiceName = "ServiceName", ServiceDescription = "ServiceDescription"})!,
                        Method = "PUT" ,
                        Rel = "Edit"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("DeleteService", "ServiceManager", new {serviceId = serviceId})!,
                        Method = "DELETE" ,
                        Rel = "Delete"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("ConfirmService", "ServiceManager", new {serviceId = serviceId})!,
                        Method = "PUT" ,
                        Rel = "Confirm"
                    },
                }
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Service
{
    public class ApiConfirmServiecResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiConfirmServiecResult Result(Guid serviceId, IUrlHelper url)
        {
            return new ApiConfirmServiecResult
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
                    }
                }
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Service
{
    public class ApiEditServiecResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiEditServiecResult Result(Guid serviceId, IUrlHelper url)
        {
            return new ApiEditServiecResult
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
                        Href = url.ActionLink("DeleteService", "ServiceManager", new {serviceId = serviceId})!,
                        Method = "DELETE" ,
                        Rel = "Delete"
                    }
                }
            };
        }
    }
}

using Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Service
{
    public class ApiGetServiceListValues
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
    }

    public class ApiGetServiceListResult
    {
        public List<ApiGetServiceListValues> Services { get; set; } = [];

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetServiceListResult Result(List<GetServiceListResultDto>? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetServiceListResult();

            return new ApiGetServiceListResult
            {
                Services = resultDto.Select(s => new ApiGetServiceListValues
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    ServiceIsActive = s.ServiceIsActive
                }).ToList(),

                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetServiceDetail", "ServiceManager", new {serviceId = "ServiceId"})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                }
            };
        }
    }
}

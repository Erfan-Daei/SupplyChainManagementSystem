using Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Service
{
    public class ApiGetServiceDetailSupplierCompanyValues
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
    public class ApiGetServiceDetailResult
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; }
        public bool ServiceIsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Creator { get; set; }
        public List<ApiGetServiceDetailSupplierCompanyValues> SupplierCompanies { get; set; } = [];
        public int SupplyRelationCount { get; set; }

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetServiceDetailResult Result(Guid serviceId, GetServiceDetailResultDto? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetServiceDetailResult();

            return new ApiGetServiceDetailResult
            {
                ServiceName = resultDto.ServiceName,
                ServiceDescription = resultDto.ServiceDescription,
                ServiceIsActive = resultDto.ServiceIsActive,
                ServiceIsConfirmed = resultDto.ServiceIsConfirmed,
                CreatedAt = resultDto.CreatedAt,
                Creator = resultDto.Creator,
                SupplyRelationCount = resultDto.SupplyRelationCount,
                SupplierCompanies = resultDto.SupplierCompanies.Select(sc => new ApiGetServiceDetailSupplierCompanyValues
                {
                    CompanyId = sc.CompanyId,
                    CompanyName = sc.CompanyName
                }).ToList(),

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
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyeId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "CompanyDetail"
                    },
                }
            };
        }
    }
}

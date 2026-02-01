using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation
{
    public class ApiConfirmSupplyRelationResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiConfirmSupplyRelationResult Result(Guid supplyRelationId, IUrlHelper url)
        {
            return new ApiConfirmSupplyRelationResult
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationDetail", "SupplyRelationManager", new {supplyRelationId = supplyRelationId})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                }
            };
        }
    }
}

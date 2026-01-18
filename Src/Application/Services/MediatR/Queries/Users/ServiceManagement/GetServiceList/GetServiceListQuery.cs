using Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceList;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceList
{
    //MediatR query for GetServiceListService
    public record GetServiceListQuery : IRequest<ResultDto<List<GetServiceListResultDto>>>;

    //MediatR queryHandler for GetServiceListService
    public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, ResultDto<List<GetServiceListResultDto>>>
    {
        private readonly IGetServiceList _getServiceList;
        public GetServiceListQueryHandler(IGetServiceList getServiceList)
        {
            _getServiceList = getServiceList;
        }

        public async Task<ResultDto<List<GetServiceListResultDto>>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
        {
            return await _getServiceList.GetServiceListAsync(request, cancellationToken);
        }
    }
}

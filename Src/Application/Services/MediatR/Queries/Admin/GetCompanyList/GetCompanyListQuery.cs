using Application.Services.Implement.Queries.Admin.GetCompanyList;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Queries.Admin.GetCompanyList
{
    //MediatR Command For GetCompanyListService
    public record GetCompanyListQuery : IRequest<ResultDto<List<GetCompanyListResultDto>>>;

    //MediatR Command Handler for GetCompanyListService
    public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, ResultDto<List<GetCompanyListResultDto>>>
    {
        private readonly IGetCompanyList _getCompanyList;
        public GetCompanyListQueryHandler(IGetCompanyList getCompanyList)
        {
            _getCompanyList = getCompanyList;
        }
        public async Task<ResultDto<List<GetCompanyListResultDto>>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
        {
            return await _getCompanyList.GetCompanyListAsync(request, cancellationToken);
        }
    }
}

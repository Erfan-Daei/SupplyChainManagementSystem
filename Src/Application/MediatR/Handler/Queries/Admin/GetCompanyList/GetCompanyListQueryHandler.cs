using Application.Dtos.Services.Queries.Admin.GetCompanyList;
using Application.Interfaces.Services.Queries.Admin.GetCompanyList;
using Application.MediatR.Services.Queries.Admin.GetCompanyList;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Queries.Admin.GetCompanyList
{
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

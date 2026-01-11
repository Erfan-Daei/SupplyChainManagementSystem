using Application.Dtos.Services.Queries.Admin.GetCompanyList;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Queries.Admin.GetCompanyList
{
    //MediatR Command For GetCompanyListService
    public record GetCompanyListQuery : IRequest<ResultDto<List<GetCompanyListResultDto>>>;
}

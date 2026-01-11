using Application.Dtos.Services.Queries.Admin.GetCompanyList;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Services.Queries.Admin.GetCompanyList;
using Application.MediatR.Services.Queries.Admin.GetCompanyList;
using Common.Output;
using System.Net;

namespace Application.Services.Queries.Admin.GetCompanyList
{
    public class GetCompanyListService : IGetCompanyList
    {
        private readonly ICompanyRepository_Query _company_Query;   //GetCompanyListAsync
        public GetCompanyListService(ICompanyRepository_Query company_Query)
        {
            _company_Query = company_Query;
        }
        public async Task<ResultDto<List<GetCompanyListResultDto>>> GetCompanyListAsync(GetCompanyListQuery request, CancellationToken ct)
        {
            try
            {
                var CompanyList = await _company_Query.GetCompanyListAsync();

                var MappedCompanyList = CompanyList?.Select(c => new GetCompanyListResultDto
                {
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName,
                }).ToList() ?? [];

                return ResultDto<List<GetCompanyListResultDto>>.Succeeded(MappedCompanyList, "لیست تمامی شرکت ها", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetCompanyListResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

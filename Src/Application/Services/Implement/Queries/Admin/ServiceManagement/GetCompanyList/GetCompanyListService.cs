using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyList;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyList
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

                return ResultDto<List<GetCompanyListResultDto>>.Succeeded(MappedCompanyList, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetCompanyListResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

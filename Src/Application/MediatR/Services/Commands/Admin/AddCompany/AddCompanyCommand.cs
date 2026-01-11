using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.Admin.AddCompany
{
    //MediatR Command for AddCompanyService
    public record AddCompanyCommand(string companyName) : IRequest<ResultDto<Guid>>;
}

using Application.Services.Implement.Commands.Admin;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.Admin.AddCompany
{
    //MediatR Command for AddCompanyService
    public record AddCompanyCommand(string companyName) : IRequest<ResultDto<Guid>>;

    //MediatR CommandHandler for AddCompanyService
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, ResultDto<Guid>>
    {
        private readonly IAddCompany _addCompany;
        public AddCompanyCommandHandler(IAddCompany addCompany)
        {
            _addCompany = addCompany;
        }
        public async Task<ResultDto<Guid>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _addCompany.AddCompanyAsync(request, cancellationToken);
        }
    }
}

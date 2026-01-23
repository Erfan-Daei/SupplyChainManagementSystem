using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmSupplyRelation;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.ConfirmSupplyRelation
{
    public class ConfirmSupplyRelationService : IConfirmSupplyRelation
    {
        private readonly ISupplyRelationRepository_Query _supplyRelation_Query;   //GetSupplyRelationByIdAsync
        private readonly ISupplyRelationRepository_Command _supplyRelation_Command;   //SaveChangesAsync
        public ConfirmSupplyRelationService(ISupplyRelationRepository_Query supplyRelation_Query
            , ISupplyRelationRepository_Command supplyRelation_Command)
        {
            _supplyRelation_Query = supplyRelation_Query;
            _supplyRelation_Command = supplyRelation_Command;
        }
        public async Task<ResultDto> ConfirmSupplyRelationAsync(ConfirmSupplyRelationCommandRequest request, CancellationToken ct)
        {
            try
            {
                var supplyRelation = await _supplyRelation_Query.GetSupplyRelationByIdAsync(request.supplyRelationId);
                if (supplyRelation == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.SupplyRelationNotFound, HttpStatusCode.NotFound);

                supplyRelation.ConfirmSupplyRelation();

                await _supplyRelation_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.SupplyRelationConfirmed, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

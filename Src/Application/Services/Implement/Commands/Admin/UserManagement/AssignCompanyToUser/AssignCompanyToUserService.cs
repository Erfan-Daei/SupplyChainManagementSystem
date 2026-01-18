using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser;
using Common.Output;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.UserManagement.AssignCompanyToUser
{
    public class AssignCompanyToUserService : IAssignCompanyToUser
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync
        private readonly IUserRepository_Command _user_Command;   //SaveChangesAsync
        public AssignCompanyToUserService(IUserRepository_Query user_Query
            , IUserRepository_Command user_Command)
        {
            _user_Query = user_Query;
            _user_Command = user_Command;
        }
        public async Task<ResultDto> AssignCompanyToUserAsync(AssignCompanyToUserCommandRequest request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserByIdAsync(request.userId);
                if (user == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.UserNotFound, HttpStatusCode.NotFound);

                User.AssignCompany(user, request.companyId);

                await _user_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.UserCompanyUpdated, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

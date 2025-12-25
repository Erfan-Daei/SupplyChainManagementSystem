using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.Hashing;

namespace Application.Services.Commands.SignIn
{
    //side class to contain all SignInService Dependencies
    public class SignInServiceDependency
    {
        public IUserRepository_Query user_Query { get; }   //CehckEmailExistAsync
        public ICompanyRepository_Query company_Query { get; }   //FindCompanyByIdAsync
        public IRoleRepository_Query role_Query { get; }   //GetRoleByNameAsync
        public IUserRepository_Command user_Command { get; }   //CreateUser
        public IHashManager hashManager { get; }   //HashPassword
        public SignInServiceDependency(IUserRepository_Query _user_Query,
            ICompanyRepository_Query _company_Query,
            IRoleRepository_Query _role_Query,
            IUserRepository_Command _user_Command,
            IHashManager _hashManager)
        {
            user_Query = _user_Query;
            company_Query = _company_Query;
            role_Query = _role_Query;
            user_Command = _user_Command;
            hashManager = _hashManager;
        }
    }
}

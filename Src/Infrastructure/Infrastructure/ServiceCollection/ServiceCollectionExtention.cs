using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.EmailManagement;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Application.Interfaces.Services.Commands.SignIn;
using Application.Services.Commands.ConfirmationEmail.SendConfirmationEmail;
using Application.Services.Commands.ConfirmationEmail.VerifyConfirmationEmail;
using Application.Services.Commands.SignIn;
using Application.Validators.Commands;
using FluentValidation;
using Infrastructure.EmailManagement;
using Infrastructure.Hashing;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Persistence.ServiceRepository.Commands.UserManagementRepository;
using Persistence.ServiceRepository.Queries.ServiceManagementRepository;
using Persistence.ServiceRepository.Queries.UserManagementRepository;

namespace Infrastructure.ServiceCollection
{
    public static class ServiceCollectionExtention
    {
        //Application layer User services
        public static IServiceCollection Application_User_Services(this IServiceCollection services)
        {
            services.AddScoped<ISignIn, SignInService>();

            services.AddScoped<ISendConfirmationEmail, SendConfirmationEmailService>();

            services.AddScoped<IVerifyConfirmationEmail, VerifyConfirmationEmail>();

            return services;
        }

        //service requests Dto Validators
        public static IServiceCollection FluentValidator_Services(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<SignInServiceValidator>();

            return services;
        }

        //Persistence layer services
        public static IServiceCollection Database_Services(this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();   //register DatabaseContext implement
            services.AddScoped<DatabaseContextAuditManager>();

            //ServiceRepository Command services
            services.AddScoped<IUserRepository_Command, UserRepository_Command>();

            //ServiceRepository Query services
            services.AddScoped<IUserRepository_Query, UserRepository_Query>();
            services.AddScoped<IRoleRepository_Query, RoleRepository_Query>();
            services.AddScoped<ICompanyRepository_Query, CompanyRepository_Query>();

            return services;
        }

        //Hash services
        public static IServiceCollection Hashing_Services(this IServiceCollection services)
        {
            services.AddTransient<IHashManager, HashManagerService>();

            return services;
        }

        //Email services
        public static IServiceCollection EmailManagement_services(this IServiceCollection services)
        {
            services.AddTransient<IEmailManager, EmailManagerService>();
            services.AddScoped<SmtpSettings>();   //POCO class To bind SmtpSettings from appsettings.json
            services.AddScoped<ConfirmationEmailSettings>();   //POCO class to bind ConfirmationEmailSettings from appsettings.json

            return services;
        }
    }
}

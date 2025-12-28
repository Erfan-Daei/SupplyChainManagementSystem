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
using Infrastructure.EmailManagement.Requirements;
using Infrastructure.Hashing;
using Infrastructure.Interfaces.EmailManagement.Requirements;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Persistence.Interface.DatabaseManagement.DatabaseConfiguration;
using Persistence.ServiceRepository.Commands.UserManagementRepository;
using Persistence.ServiceRepository.Queries.ServiceManagementRepository;
using Persistence.ServiceRepository.Queries.UserManagementRepository;

namespace Infrastructure.ServiceCollection
{
    public static class ServiceCollectionExtention
    {
        //Application layer User services
        public static IServiceCollection Application_Services(this IServiceCollection services)
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
            services.AddScoped<IDatabaseContext, DatabaseContext>();   //register DatabaseContext implement
            services.AddScoped<IDatabaseContextAuditManager, DatabaseContextAuditManager>();

            //ServiceRepository Command services
            services.AddScoped<IUserRepository_Command, UserRepository_Command>();

            //ServiceRepository Query services
            services.AddScoped<IUserRepository_Query, UserRepository_Query>();
            services.AddScoped<IRoleRepository_Query, RoleRepository_Query>();
            services.AddScoped<ICompanyRepository_Query, CompanyRepository_Query>();

            return services;
        }

        //]دبقشسفقعزفعقث services
        public static IServiceCollection Infrastructure_services(this IServiceCollection services)
        {
            services.AddTransient<IHashManager, HashManagerService>();

            services.AddScoped<IEmailManager, EmailManagerService>();
            services.AddScoped<ISmtpClientConfiguration, SmtpClientConfiguration>();
            services.AddScoped<ISmtpMessageProvider, SmtpMessageProvider>();
            services.AddScoped<ITemplateProvider, TemplateProvider>();

            services.AddSingleton<SmtpSettings>();   //POCO class To bind SmtpSettings from appsettings.json
            services.AddSingleton<ConfirmationEmailSettings>();   //POCO class to bind ConfirmationEmailSettings from appsettings.json

            return services;
        }
    }
}

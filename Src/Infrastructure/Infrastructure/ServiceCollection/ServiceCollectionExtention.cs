using Application.Dtos.JWT;
using Application.Dtos.Services.Commands.ConfirmationEmail.SendConfirmationEmail;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.EmailManagement;
using Application.Interfaces.HashManagement;
using Application.Interfaces.JWT;
using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Application.Interfaces.Services.Commands.LogIn;
using Application.Interfaces.Services.Commands.LogOut;
using Application.Interfaces.Services.Commands.RefreshToken;
using Application.Interfaces.Services.Commands.SignUp;
using Application.MediatR.Handler.Commands.SignUp;
using Application.MediatR.Services.Commands.SignUp;
using Application.Services.Commands.ConfirmationEmail.SendConfirmationEmail;
using Application.Services.Commands.ConfirmationEmail.VerifyConfirmationEmail;
using Application.Services.Commands.LogIn;
using Application.Services.Commands.LogOut;
using Application.Services.Commands.RefreshToken;
using Application.Services.Commands.SignUp;
using Application.Validators.Commands.SignUp;
using FluentValidation;
using Infrastructure.EmailManagement;
using Infrastructure.EmailManagement.Requirements;
using Infrastructure.Hashing;
using Infrastructure.Interfaces.EmailManagement.Requirements;
using Infrastructure.JWT;
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
        //register Application layer services
        public static IServiceCollection Application_Services(this IServiceCollection services)
        {
            services.AddScoped<ISignUp, SignUpService>();

            services.AddScoped<ISendConfirmationEmail, SendConfirmationEmailService>();

            services.AddScoped<IVerifyConfirmationEmail, VerifyConfirmationEmailService>();

            services.AddScoped<ILogIn, LogInService>();

            services.AddScoped<ILogOut, LogOutService>();

            services.AddScoped<IRefreshToken, RefreshTokenService>();
            return services;
        }

        //register MediatR Command/Queries and Handlers
        public static IServiceCollection MediatR_Services(this IServiceCollection services)
        {
            services.AddMediatR(mr =>
                mr.RegisterServicesFromAssemblies
                (
                    typeof(SignUpCommand).Assembly,
                    typeof(SignUpCommandHandler).Assembly
                )
            );

            return services;
        }

        //register FluentValidation Validators
        public static IServiceCollection FluentValidation_Services(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(SignUpValidator).Assembly);

            return services;
        }

        //register Persistence layer services
        public static IServiceCollection Persistense_Services(this IServiceCollection services)
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

        //register Infrastructure layer services
        public static IServiceCollection Infrastructure_services(this IServiceCollection services)
        {
            services.AddTransient<IHashManager, HashManagerService>();

            services.AddScoped<IEmailManager, EmailManagerService>();
            services.AddScoped<ISmtpClientConfiguration, SmtpClientConfiguration>();
            services.AddScoped<ISmtpMessageProvider, SmtpMessageProvider>();
            services.AddScoped<ITemplateProvider, TemplateProvider>();

            services.AddSingleton<SmtpSettings>();   //POCO class To bind SmtpSettings from appsettings.json
            services.AddSingleton<ConfirmationEmailSettings>();   //POCO class to bind ConfirmationEmailSettings from appsettings.json

            services.AddScoped<IJwtTokenManager, JwtTokenManager>();

            services.AddSingleton<JwtSettings>();   //POCO class To bind JwtSettings from appsettings.json
            services.AddSingleton<RefreshTokenSettings>();   //POCO class To bind RefreshTokenSettings from appsettings.json

            return services;
        }
    }
}

using Application.Dtos.EmailManagement;
using Application.Dtos.JWT;
using Application.Interfaces.Auth;
using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.EmailManagement;
using Application.Interfaces.HashManagement;
using Application.Interfaces.JWT;
using Application.Services.Implement.Commands.Admin.ServiceManagement.AddCompany;
using Application.Services.Implement.Commands.Admin.ServiceManagement.AddService;
using Application.Services.Implement.Commands.Admin.ServiceManagement.AddSupplyRelation;
using Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteCompany;
using Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteService;
using Application.Services.Implement.Commands.Admin.ServiceManagement.EditCompany;
using Application.Services.Implement.Commands.Admin.ServiceManagement.EditService;
using Application.Services.Implement.Commands.Admin.UserManagement.AssignCompanyToUser;
using Application.Services.Implement.Commands.Admin.UserManagement.DemoteUserRole;
using Application.Services.Implement.Commands.Admin.UserManagement.PromoteUserRole;
using Application.Services.Implement.Commands.Users.UserManagement.ConfirmationEmail.SendConfirmationEmail;
using Application.Services.Implement.Commands.Users.UserManagement.ConfirmationEmail.VerifyConfirmationEmail;
using Application.Services.Implement.Commands.Users.UserManagement.LogIn;
using Application.Services.Implement.Commands.Users.UserManagement.LogOut;
using Application.Services.Implement.Commands.Users.UserManagement.RefreshToken;
using Application.Services.Implement.Commands.Users.UserManagement.SignUp;
using Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyList;
using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail;
using Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceList;
using Application.Services.Implement.Queries.Users.UserManagement.GetUserDetail;
using Application.Services.Implement.Queries.Users.UserManagement.GetUserList;
using Application.Services.MediatR.Commands.User.UserManagement.SignUp;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.EmailManagement.Requirements;
using Infrastructure.EmailManagement.Requirements.SmtpClientConfiguration;
using Infrastructure.EmailManagement.Requirements.SmtpMessageProvider;
using Infrastructure.EmailManagement.Requirements.TemplateProvider;
using Infrastructure.EmailManagement.Service;
using Infrastructure.Hashing;
using Infrastructure.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration.AuditManager;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.ServiceRepository.Commands.ServiceManagementRepository;
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

            services.AddScoped<IAddCompany, AddCompanyService>();

            services.AddScoped<IGetCompanyList, GetCompanyListService>();

            services.AddScoped<IGetCompanyDetail, GetCompanyDetailService>();

            services.AddScoped<IEditCompany, EditCompanyService>();

            services.AddScoped<IPromoteUserRole, PromoteUserRoleService>();

            services.AddScoped<IDemoteUserRole, DemoteUserRoleService>();

            services.AddScoped<IAssignCompanyToUser, AssignCompanyToUserService>();

            services.AddScoped<IDeleteCompany, DeleteCompanyService>();

            services.AddScoped<IAddService, AddServiceService>();

            services.AddScoped<IGetServiceDetail, GetServiceDetailService>();

            services.AddScoped<IGetServiceList, GetServiceListService>();

            services.AddScoped<IEditService, EditServiceService>();

            services.AddScoped<IDeleteService, DeleteServiceService>();

            services.AddScoped<IGetUserDetail, GetUserDetailService>();

            services.AddScoped<IGetUserList, GetUserListService>();

            services.AddScoped<IAddSupplyRelation, AddSupplyRelationService>();

            services.AddScoped<IGetSupplyRelationDetail, GetSupplyRelationDetailService>();

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
            services.AddScoped<ICompanyRepository_Command, CompanyRepository_Command>();
            services.AddScoped<IServiceRepository_Command, ServiceRepository_Command>();
            services.AddScoped<IServiceRepository_Query, ServiceRepository_Query>();
            services.AddScoped<ISupplyRelationRepository_Command, SupplyRelationRepository_Command>();
            services.AddScoped<ISupplyRelationRepository_Query, SupplyRelationRepository_Query>();

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

            services.AddScoped<IAuthManager, AuthManager>();

            return services;
        }

        public static IServiceCollection AddAppsettingsJsonBinds(this IServiceCollection services, IConfiguration configuration)
        {
            //bind appseting.json ConfirmationEmailSettings to POCO class
            services.AddSingleton(sp =>
                configuration.GetSection("ConfirmationEmailSettings")
                .Get<ConfirmationEmailSettings>() ?? new ConfirmationEmailSettings()
            );

            //bind appseting.json ConfirmationEmailPath to POCO class
            services.AddSingleton(sp =>
                configuration.GetSection("ConfirmationEmailPath")
                .Get<ConfirmationEmailPath>() ?? new ConfirmationEmailPath()
            );

            //bind appseting.json SmtpSettings to POCO class
            services.AddSingleton(sp =>
                configuration.GetSection("SmtpSettings")
                .Get<SmtpSettings>() ?? new SmtpSettings()
            );

            //bind appsettings.json JwtSetting to POCO class
            services.AddSingleton(sp =>
                configuration.GetSection("JwtSettings")
                .Get<JwtSettings>() ?? new JwtSettings()
            );

            //bind appsettings.json RefreshTokenSetting to POCO class
            services.AddSingleton(sp =>
                configuration.GetSection("RefreshTokenSettings")
                .Get<RefreshTokenSettings>() ?? new RefreshTokenSettings()
            );

            return services;
        }
    }
}

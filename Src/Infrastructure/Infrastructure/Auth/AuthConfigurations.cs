using Domain.Entities.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Auth
{
    //class to configure all Athentication and Authorization options
    public static class AuthConfigurations
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("AdminsOnly", policy =>
                {
                    policy.RequireRole(SeedRoles.AdminName, SeedRoles.CompanyAdminName);
                });

            return services;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var JwtSettings = configuration.GetRequiredSection("JwtSettings");
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtSettings["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = JwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["Key"]!)),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RoleClaimType = ClaimTypes.Role,
                    };

                    configureOptions.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };

                    configureOptions.SaveToken = true;
                });

            return services;
        }
    }
}

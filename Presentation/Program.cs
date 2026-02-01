using Application.Interfaces.Database.DatabaseConfiguration;
using FluentValidation.AspNetCore;
using Infrastructure.Auth;
using Infrastructure.ServiceCollection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Presentation.Services.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//add Authorization and Authentication configuration
builder.Services.AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();   //add FluentValidation automation

//register all services from different layers
builder.Services.Application_Services()
    .MediatR_Services()
    .FluentValidation_Services()
    .Persistense_Services()
    .Infrastructure_services()
    .AddAppsettingsJsonBinds(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDatabaseContext_UserInfo, DatabaseContext_UserInfo>();

//to avoid conflict between integration test and main program
if (!builder.Environment.IsEnvironment("IntegartionTest"))
    builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//for Authorize
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Security = new List<OpenApiSecurityRequirement>();

        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Enter: Bearer {Your Token}"
        };
        document.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer", document, null)
                {
                    Reference = new OpenApiReferenceWithDescription
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });

        return Task.CompletedTask;
    });
});

builder.Services.AddEndpointsApiExplorer();

//swagger Authorization
/*builder.Services.AddSwaggerGen(sw =>
{
    //add Jwt Authentication to swagger
    sw.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authentication",
        Description = "Insert Your Jwt Token Value",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    
    sw.AddSecurityRequirement(sr => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", null, null)
            {
                Reference = new OpenApiReferenceWithDescription
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //for swagger
    /*app.UseSwagger();
    app.UseSwaggerUI();*/

    //for Scalar
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

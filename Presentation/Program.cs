using Application.Interfaces.Database.DatabaseConfiguration;
using FluentValidation.AspNetCore;
using Infrastructure.Auth;
using Infrastructure.ServiceCollection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Presentation.Services.Database;

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

builder.Services.AddScoped<IDatabaseContext_UserInfo, DatabaseContext_UserInfo>();

//to avoid conflict between integration test and main program
if (!builder.Environment.IsEnvironment("IntegartionTest"))
    builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// for Swagger
builder.Services.AddEndpointsApiExplorer();
//add scalar instead of swagger
builder.Services.AddSwaggerGen(sw =>
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

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //for swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

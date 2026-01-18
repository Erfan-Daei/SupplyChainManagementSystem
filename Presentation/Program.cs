using Application.Interfaces.Database.DatabaseConfiguration;
using FluentValidation.AspNetCore;
using Infrastructure.Auth;
using Infrastructure.ServiceCollection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Presentation.Services.Database;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//add Authorization and Authentication configuration
builder.Services.AddAuthorizationPolicies()
    .AddJwtAuthentication(builder.Configuration);

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
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    //add Jwt Authentication to swagger
    var security = new OpenApiSecurityScheme
    {
        Name = "JwtAuthentication",
        Description = "Insert Your Jwt Token Value",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWt",
    };

    var securityReference = new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme)
    {
        Reference = new OpenApiReferenceWithDescription
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    sw.AddSecurityDefinition(securityReference.Reference.Id, security);
    sw.AddSecurityRequirement(sr => new OpenApiSecurityRequirement
    {
        {securityReference, new List<string>{ } }
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

using Application.Dtos.Services.Commands.ConfirmationEmail.SendConfirmationEmail;
using Application.Interfaces.Database.DatabaseConfiguration;
using FluentValidation.AspNetCore;
using Infrastructure.EmailManagement.Requirements;
using Infrastructure.ServiceCollection;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Presentation.Services.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();   //add FluentValidation automation

//register all services from different layers
builder.Services.Application_Services()
    .MediatR_Services()
    .FluentValidation_Services()
    .Persistense_Services()
    .Infrastructure_services();

//bind appseting.json ConfirmationEmailSettings to POCO class
builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("ConfirmationEmailSettings")
    .Get<ConfirmationEmailSettings>() ?? new ConfirmationEmailSettings()
);

//bind appseting.json ConfirmationEmailPath to POCO class
builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("ConfirmationEmailPath")
    .Get<ConfirmationEmailPath>() ?? new ConfirmationEmailPath()
);

//bind appseting.json SmtpSettings to POCO class
builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("SmtpSettings")
    .Get<SmtpSettings>() ?? new SmtpSettings()
);

builder.Services.AddScoped<IDatabaseContext_UserInfo, DatabaseContext_UserInfo>();

//to avoid conflict between integration test and main program
if (!builder.Environment.IsEnvironment("IntegartionTest"))
{
    builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
}

// for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //for swagger
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

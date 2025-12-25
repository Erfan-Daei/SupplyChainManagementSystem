using Application.Interfaces.Database.DatabaseConfiguration;
using Application.Services.Commands.ConfirmationEmail;
using FluentValidation.AspNetCore;
using Infrastructure.EmailManagement;
using Infrastructure.ServiceCollection;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Presentation.Services.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();   //FluentValidation

builder.Services.AddHttpContextAccessor();

builder.Services.Application_User_Services()   //all App layer services
    .Database_Services()  //all Persistence layer services
    .Hashing_Services()   //all Hashing services
    .EmailManagement_services();   //all EmailManagement services

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));   //bind appseting.json SmtpSettings to POCO class
builder.Services.Configure<ConfirmationEmailSettings>(builder.Configuration.GetSection("ConfirmationEmailSettings"));   //bind appseting.json ConfirmationEmailSettings to POCO class
builder.Services.AddScoped<IDatabaseContext_UserInfo, DatabaseContext_UserInfo>();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

using Application.Dtos.Services.Commands.SignUp;
using Application.MediatR.Services.Commands.SignUp;
using Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Presentation.Output.Base;
using Presentation_Test.Setup.Factory;
using System.Net;
using System.Net.Http.Json;

namespace Presentation_Test.Services.Controllers.Area.User.UserManagement.Auth
{
    public class SignUpTest : IClassFixture<WebApplicationFactory_Test>
    {
        private readonly HttpClient _client;
        private readonly IMediator _mediator;
        private readonly WebApplicationFactory_Test _factory;
        private readonly DatabaseContext _db;
        public SignUpTest(WebApplicationFactory_Test factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();   //test client
            var scope = _factory.Services.CreateScope();
            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            _db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        [Fact]
        public async Task SignUpAsync_SuccessFull()
        {
            //arrange
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = SeedCompanies.DefaultCompanyId,
            });

            //act
            var response = await _client.PostAsJsonAsync("/api/User/UserManagement/Auth/SingUp", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResultDto>();

            //assert
            Assert.True(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            //assert database
            //assert User Table
            var user = _db.Users.FirstOrDefault(u => u.UserEmail == request.Dto.UserEmail);
            Assert.NotNull(user);
            Assert.Equal(request.Dto.UserFullName, user!.UserFullName);

            //assert UserInRole Table
            var userInRole = _db.UserInRoles.FirstOrDefault(u => u.UserId == user.UserId);
            Assert.NotNull(userInRole);
            Assert.Equal(SeedRoles.ViewerId, userInRole.RoleId);

            //check password is hashed and is hashed from request password
            Assert.NotEqual(request.Dto.Password, user.UserPassword);
            Assert.True(BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.UserPassword));

            //assert userId in CreateAt url
            Assert.Equal(response.Headers.Location!.Query, $"?userId={user.UserId}");
        }

        [Fact]
        public async Task SignUpAsync_Gives_FluentValidations_WrongType_Errors()
        {
            //arrange
            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Wrong_Fullname",
                UserEmail = "WrongEmail",
                Password = "WrongPassword",
                ConPassword = "WrongConPassword",
                CompanyId = Guid.Empty,   //wrong CompanyId
            });

            //act
            var response = await _client.PostAsJsonAsync("/api/User/UserManagement/Auth/SingUp", request);
            var problems = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            //assert validation errors
            Assert.True(problems!.Errors.ContainsKey("Dto.UserFullName"));
            Assert.True(problems!.Errors.ContainsKey("Dto.UserEmail"));
            Assert.True(problems!.Errors.ContainsKey("Dto.Password"));
            Assert.True(problems!.Errors.ContainsKey("Dto.CompanyId"));

            Assert.Equal("لطفا نام و نام خانوادگی را به درستی وارد کنید", problems.Errors["Dto.UserFullName"].First());
            Assert.Equal("لطفا ایمیل خودرا به درستی وارد کنید", problems.Errors["Dto.UserEmail"].First());
            Assert.Equal("لطفا آی دی شرکت خود را وارد کنید", problems.Errors["Dto.CompanyId"].First());
            Assert.Contains("رمزعبور و تکرار آن برابر نیست", problems.Errors["Dto.Password"].ToList());
        }

        [Fact]
        public async Task SignUpAsync_Gives_FluentValidation_NullInput_Error()
        {
            //arrange
            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = string.Empty,
                UserEmail = string.Empty,
                Password = string.Empty,
                ConPassword = string.Empty,
                CompanyId = Guid.Empty,   //wrong CompanyId
            });

            //act
            var response = await _client.PostAsJsonAsync("/api/User/UserManagement/Auth/SingUp", request);
            var problems = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            //assert validation errors
            Assert.True(problems!.Errors.ContainsKey("Dto.UserFullName"));
            Assert.True(problems!.Errors.ContainsKey("Dto.UserEmail"));
            Assert.True(problems!.Errors.ContainsKey("Dto.Password"));
            Assert.True(problems!.Errors.ContainsKey("Dto.CompanyId"));

            Assert.Equal("لطفا نام و نام خانوادگی خود را وارد کنید", problems.Errors["Dto.UserFullName"].First());
            Assert.Equal("لطفا ایمیل خود را وارد کنید", problems.Errors["Dto.UserEmail"].First());
            Assert.Equal("لطفا آی دی شرکت خود را وارد کنید", problems.Errors["Dto.CompanyId"].First());
            Assert.Contains("لطفا رمزعبور را وارد کنید", problems.Errors["Dto.Password"].ToList());
        }

        [Fact]
        public async Task SignUpAsync_Gives_Email_Exist_Error()
        {
            //arrange
            //add default User to get Email Exist error
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            var defUser = Domain.Entities.UserManagement.User.Create("Test", "Test@gmail.com", "12345Ed@", SeedCompanies.DefaultCompanyId);
            var defaultUser = _db.Users.Add(defUser);
            _db.SaveChanges();


            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = SeedCompanies.DefaultCompanyId,
            });

            //act
            var result = await _mediator.Send(request);

            //assert
            Assert.False(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
            Assert.Equal("ایمیل تکراری است، لطفا یک ایمیل دیگر انتخاب کنید", result.Message);

            //assert database
            //check new user didnt add to database
            var user = _db.Users.Where(u => u.UserEmail == request.Dto.UserEmail)
                .ToList();

            Assert.NotNull(user);
            Assert.True(user.Count == 1);
        }

        [Fact]
        public async Task SignUpAsync_Company_Not_Exist_Error()
        {
            //arrange
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = await _mediator.Send(request);

            //assert
            Assert.False(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

            //assert database
            //check new user didnt add to database
            var user = _db.Users.FirstOrDefault(u => u.UserEmail == request.Dto.UserEmail);
            Assert.Null(user);
        }

        [Fact]
        public async Task SignUpAsync_Repository_Exception_Error()
        {
            //arrange
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = string.Empty,
                UserEmail = string.Empty,
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = SeedCompanies.DefaultCompanyId,
            });

            //act
            var result = await _mediator.Send(request);

            //assert
            Assert.False(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal(Guid.Empty, result.Data);

            //assert database
            //check new user didnt add to database
            var user = _db.Users.FirstOrDefault(u => u.UserEmail == request.Dto.UserEmail);
            Assert.Null(user);
        }
    }
}

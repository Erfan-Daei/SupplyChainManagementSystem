using Application.Dtos.Services.Commands.SignUp;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.MediatR.Services.Commands.SignUp;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Presentation.Output.Base;
using Presentation_Test.Setup.Factory;
using System.Net;
using System.Net.Http.Json;

namespace Presentation_Test.Services.Controllers.Area.User.UserManagement.SignUp
{
    public class SignUpAsyncTest : IClassFixture<WebApplicationFactory_Test>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory_Test _factory;
        public SignUpAsyncTest(WebApplicationFactory_Test factory)
        {
            _factory = factory;
            _client = factory.CreateClient();   //test client
        }

        [Fact]
        public async Task SignUpAsync_SuccessFull()
        {
            //arrange
            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = SeedCompanies.DefaultCompanyId,
            });

            //act
            var response = await _client.PostAsJsonAsync("/api/User/Usermanagement/Signup", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResultDto>();

            //assert
            Assert.True(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            //assert database
            //assert User Table
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var user = db.Users.FirstOrDefault(u => u.UserEmail == request.Dto.UserEmail);
            Assert.NotNull(user);
            Assert.Equal(request.Dto.UserFullName, user!.UserFullName);

            //assert UserInRole Table
            var userInRole = db.UserInRoles.FirstOrDefault(u => u.UserId == user.UserId);
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
            var response = await _client.PostAsJsonAsync("/api/User/Usermanagement/Signup", request);
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
            var response = await _client.PostAsJsonAsync("/api/User/Usermanagement/Signup", request);
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
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var defUser = Domain.Entities.UserManagement.User.Create("Test", "Test@gmail.com", "12345Ed@", SeedCompanies.DefaultCompanyId);
            var defaultUser = db.Users.Add(defUser);
            db.SaveChanges();

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = SeedCompanies.DefaultCompanyId,
            });

            //act
            var response = await _client.PostAsJsonAsync("/api/User/Usermanagement/Signup", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResultDto>();

            //assert
            Assert.False(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
            Assert.Equal("ایمیل تکراری است، لطفا یک ایمیل دیگر انتخاب کنید", result.Message);

            //assert database
            //check new user didnt add to database
            var user = db.Users.Where(u => u.UserEmail == request.Dto.UserEmail)
                .ToList();

            Assert.NotNull(user);
            Assert.True(user.Count == 1);
        }

        [Fact]
        public async Task SignUpAsync_Company_Not_Exist_Error()
        {
            //arrange
            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var response = await _client.PostAsJsonAsync("/api/User/Usermanagement/Signup", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResultDto>();

            //assert
            Assert.False(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

            //assert database
            //check new user didnt add to database
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var user = db.Users.FirstOrDefault(u => u.UserEmail == request.Dto.UserEmail);
            Assert.Null(user);
        }

        [Fact]
        public async Task SignUpAsync_Repository_Exception_Error()
        {
            //arrange
            //mock for database UserRepository_Command to throw exception
            var user_CommandMock = new Mock<IUserRepository_Command>();

            user_CommandMock.Setup(uc =>
                uc.CreateUserAsync(It.IsAny<Domain.Entities.UserManagement.User>(),
                It.IsAny<UserInRole>())
            ).ThrowsAsync(new InvalidOperationException());

            //change factory to get new created UserRepository mock instead of default
            var factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //remove IUserRepository_Command
                    var descriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(IUserRepository_Command));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    //add new created mock
                    services.AddScoped<IUserRepository_Command>(_ => user_CommandMock.Object);
                });
            });

            //create new client with new configured factory
            var client = factory.CreateClient();

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = SeedCompanies.DefaultCompanyId,
            });

            //act
            var response = await client.PostAsJsonAsync("/api/User/Usermanagement/Signup", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResultDto>();

            //assert
            Assert.False(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);

            //assert database
            //check new user didnt add to database
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var user = db.Users.FirstOrDefault(u => u.UserEmail == request.Dto.UserEmail);
            Assert.Null(user);
        }
    }
}

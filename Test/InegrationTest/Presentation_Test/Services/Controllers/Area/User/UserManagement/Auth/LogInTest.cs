using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.MediatR.Services.Commands.LogIn;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Persistence.DatabaseManagement.DatabaseConfiguration;
using Presentation.Output.Area.User.UserManagement;
using Presentation.Output.Base;
using Presentation_Test.Setup.Database;
using Presentation_Test.Setup.Factory;
using System.Net;
using System.Net.Http.Json;

namespace Presentation_Test.Services.Controllers.Area.User.UserManagement.Auth
{
    public class LogInTest : IClassFixture<WebApplicationFactory_Test>
    {
        private readonly WebApplicationFactory_Test _factory;
        private readonly HttpClient _client;
        private readonly IMediator _mediator;
        private readonly DatabaseContext _db;
        public LogInTest(WebApplicationFactory_Test factorty)
        {
            _factory = factorty;
            _client = _factory.CreateClient();
            var scope = _factory.Services.CreateScope();
            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            _db = new SeedLoginData().SetSeedLoginData(scope);
        }

        [Fact]
        public async Task LogIn_Successfull()
        {
            //arrange
            var request = new LogInCommand("Test@gmail.com", "12345Ed@");

            //act
            var response = await _client.PostAsJsonAsync("/api/User/UserManagement/Auth/LogIn", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResultDto<ApiJwtTokenResultDto>>();

            //assert
            Assert.True(result!.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotEmpty(result.Data!.AccessToken);

            var user = _db.Users.FirstOrDefault();
            var refreshToken = _db.UserTokens.Single(ut => ut.UserId == user.UserId && ut.UserTokenType == UserTokenType.RefreshToken.ToString());
            Assert.NotNull(refreshToken);

            var refreshTokenCookie = response.Headers.GetValues("Set-Cookie").First(c => c.StartsWith("RefreshToken"));
            Assert.NotNull(refreshTokenCookie);
        }

        [Fact]
        public async Task LogIn_FluentValidation_Null_Input()
        {
            //arrange
            var request = new LogInCommand("WeongEmail", string.Empty);

            //act
            var response = await _client.PostAsJsonAsync("/api/User/UserManagement/Auth/LogIn", request);
            var problems = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.True(problems!.Errors.ContainsKey("UserEmail"));
            Assert.True(problems!.Errors.ContainsKey("UserPassword"));

            Assert.Equal("لطفا ایمیل خود را به درستی وارد کنید", problems.Errors["UserEmail"].First());
            Assert.Equal("لطفا رمز عبور خودرا وارد کنکید", problems.Errors["UserPassword"].First());
        }

        [Fact]
        public async Task LogIn_Email_Not_Exist_Error()
        {
            //arrange
            var request = new LogInCommand("wrong@gmail.com", "password");

            //act
            var result = await _mediator.Send(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("کاربر یافت نشد", result.Message);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task LogIn_Wrong_Password_Error()
        {
            //arrange
            var request = new LogInCommand("Test@gmail.com", "wrongPassword");

            //act
            var result = await _mediator.Send(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("رمز عبور اشتباه است", result.Message);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task LogIn_Argument_Null_Exception()
        {
            //arrange
            var request = new LogInCommand("Test@gmail.com", null!);

            //act
            var result = await _mediator.Send(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}

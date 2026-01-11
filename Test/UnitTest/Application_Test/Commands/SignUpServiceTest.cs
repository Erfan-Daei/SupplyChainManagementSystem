using Application.Dtos.Services.Commands.SignUp;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.User.SignUp;
using Application.MediatR.Services.Commands.SignUp;
using Application.Services.Commands.SignUp;
using Domain.Entities.Common;
using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;
using Moq;
using System.Net;

namespace Application_Test.Commands
{
    public class SignUpServiceTest
    {
        private readonly Mock<IUserRepository_Query> _user_QueryMock;
        private readonly Mock<IUserRepository_Command> _user_CommandMock;
        private readonly Mock<IRoleRepository_Query> _role_QueryMock;
        private readonly Mock<ICompanyRepository_Query> _company_QueryMock;
        private readonly Mock<IHashManager> _hashManagerMock;
        private readonly ISignUp _signIn;
        public SignUpServiceTest()
        {
            _user_QueryMock = new Mock<IUserRepository_Query>();
            _user_CommandMock = new Mock<IUserRepository_Command>();
            _role_QueryMock = new Mock<IRoleRepository_Query>();
            _company_QueryMock = new Mock<ICompanyRepository_Query>();
            _hashManagerMock = new Mock<IHashManager>();

            _signIn = new SignUpService
            (
                _user_QueryMock.Object,
                _company_QueryMock.Object,
                _role_QueryMock.Object,
                _user_CommandMock.Object,
                _hashManagerMock.Object
            );
        }

        [Fact]
        public async Task User_Email_Does_Exist()
        {
            //arrange
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync("test@gmail.com"))
                .ReturnsAsync(true);

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
            Assert.Equal("ایمیل تکراری است، لطفا یک ایمیل دیگر انتخاب کنید", result.Message);
        }

        [Fact]
        public async Task Company_Is_Null_When_CompanyId_Is_Wrong()
        {
            //arrange


            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Company)null);

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("شرکت مورد نظر یافت نشد، لطفا دوباره تلاش کنید", result.Message);
        }

        [Fact]
        public async Task Role_Not_Found_By_Name()
        {
            //arrange
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Role)null);

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("نقش مورد نظر یافت نشد، لطفا دوباره تلاش کنید", result.Message);
        }

        [Fact]
        public async Task Create_User_Successfull()
        {
            //arrange


            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.BCryptHashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .Returns(Task.CompletedTask);

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Equal("حساب کاربری با موفقیت ثبت شد", result.Message);
            Assert.NotEqual(Guid.Empty, result.Data);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_ArgumentNullException()
        {
            //arrange


            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.BCryptHashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new ArgumentNullException("ورودی معتبر نیست. لطفاً اطلاعات را بررسی کنید.", new Exception()));

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Contains("ورودی معتبر نیست. لطفاً اطلاعات را بررسی کنید.", result.Message);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_InvalidOperationException()
        {
            //arrange


            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.BCryptHashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new InvalidOperationException("عملیات نامعتبر بود.", new Exception()));

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("عملیات نامعتبر بود.", result.Message);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_TimeoutException()
        {
            //arrange


            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.BCryptHashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new TimeoutException("زمان اجرای عملیات دیتابیس به پایان رسید.", new Exception()));

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("زمان اجرای عملیات دیتابیس به پایان رسید.", result.Message);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_Exception()
        {
            //arrange


            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.BCryptHashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new Exception("خطای ناشناخته رخ داد.", new Exception()));

            var request = new SignUpCommand(new SignUpServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            });

            //act
            var result = await _signIn.SignUpAsync(request, default);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("خطای ناشناخته رخ داد.", result.Message);
        }
    }
}

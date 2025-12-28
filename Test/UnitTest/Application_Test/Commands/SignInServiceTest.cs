using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.SignIn;
using Application.Services.Commands.SignIn;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Net;

namespace Application_Test.Commands
{
    public class SignInServiceTest
    {
        private readonly Mock<IValidator<SignInServiceRequestDto>> _validator;
        private readonly Mock<IUserRepository_Query> _user_QueryMock;
        private readonly Mock<IUserRepository_Command> _user_CommandMock;
        private readonly Mock<IRoleRepository_Query> _role_QueryMock;
        private readonly Mock<ICompanyRepository_Query> _company_QueryMock;
        private readonly Mock<IHashManager> _hashManagerMock;
        private readonly ISignIn _signIn;
        public SignInServiceTest()
        {
            _validator = new Mock<IValidator<SignInServiceRequestDto>>();
            _user_QueryMock = new Mock<IUserRepository_Query>();
            _user_CommandMock = new Mock<IUserRepository_Command>();
            _role_QueryMock = new Mock<IRoleRepository_Query>();
            _company_QueryMock = new Mock<ICompanyRepository_Query>();
            _hashManagerMock = new Mock<IHashManager>();

            _signIn = new SignInService(_validator.Object,
                _user_QueryMock.Object,
                _user_CommandMock.Object,
                _company_QueryMock.Object,
                _role_QueryMock.Object,
                _hashManagerMock.Object);
        }

        [Fact]
        public async Task Validator_Gives_NullOrEmpty_Errors()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    // UserFullName
                    new ValidationFailure("UserFullName", "لطفا نام و نام خانوادگی خود را وارد کنید"),
                    new ValidationFailure("UserFullName", "نام و نام خانوادگی باید کم تر 50 کاراکتر باشد"),
                    new ValidationFailure("UserFullName", "لطفا نام و نام خانوادگی را به درستی وارد کنید"),

                    // UserEmail
                    new ValidationFailure("UserEmail", "لطفا ایمیل خود را وارد کنید"),
                    new ValidationFailure("UserEmail", "لطفا ایمیل خودرا به درستی وارد کنید"),
                    new ValidationFailure("UserEmail", "ایمیل نمی تواند بیش از 100 کاراکتر باشد"),

                    // Password
                    new ValidationFailure("Password", "لطفا رمزعبور را وارد کنید"),
                    new ValidationFailure("Password", "رمز عبور باید حداقل 8 کاراکتر باشد"),
                    new ValidationFailure("Password", "رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد"),
                    new ValidationFailure("Password", "رمز عبور باید حداقل یک حرف بزرگ داشته باشد"),
                    new ValidationFailure("Password", "رمز عبور باید حداقل یک حرف کوچک داشته باشد"),
                    new ValidationFailure("Password", "رمز عبور باید حداقل یک عدد داشته باشد"),
                    new ValidationFailure("Password", "رمز عبور باید حداقل یک کاراکتر خاص داشته باشد"),
                    new ValidationFailure("Password", "رمزعبور و تکرار آن برابر نیست"),

                    // CompanyId
                    new ValidationFailure("CompanyId", "لطفا آی دی شرکت خود را وارد کنید"),

                }));

            var request = new SignInServiceRequestDto();

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.Contains("لطفا نام و نام خانوادگی خود را وارد کنید", result.Message);
            Assert.Contains("ایمیل نمی تواند بیش از 100 کاراکتر باشد", result.Message);
            Assert.Contains("رمز عبور باید حداقل یک عدد داشته باشد", result.Message);
            Assert.Contains("لطفا آی دی شرکت خود را وارد کنید", result.Message);
        }

        [Fact]
        public async Task User_Email_Does_Exist()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);

            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync("test@gmail.com"))
                .ReturnsAsync(true);

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("ایمیل تکراری است، لطفا یک ایمیل دیگر انتخاب کنید", result.Message);
        }

        [Fact]
        public async Task Company_Is_Null_When_CompanyId_Is_Wrong()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Company)null);

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("شرکت مورد نظر یافت نشد، لطفا دوباره تلاش کنید", result.Message);
        }

        [Fact]
        public async Task Role_Not_Found_By_Name()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Role)null);

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("نقش مورد نظر یافت نشد، لطفا دوباره تلاش کنید", result.Message);
        }

        [Fact]
        public async Task Create_User_Successfull()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.HashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .Returns(Task.CompletedTask);

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

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
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.HashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new ArgumentNullException("ورودی معتبر نیست. لطفاً اطلاعات را بررسی کنید.", new Exception()));

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Contains("ورودی معتبر نیست. لطفاً اطلاعات را بررسی کنید.", result.Message);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_InvalidOperationException()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.HashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new InvalidOperationException("عملیات نامعتبر بود.", new Exception()));

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("عملیات نامعتبر بود.", result.Message);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_TimeoutException()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.HashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new TimeoutException("زمان اجرای عملیات دیتابیس به پایان رسید.", new Exception()));

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("زمان اجرای عملیات دیتابیس به پایان رسید.", result.Message);
        }

        [Fact]
        public async Task Create_User_UnSuccessfull_Exception()
        {
            //arrange
            _validator.Setup(v => v.Validate(It.IsAny<SignInServiceRequestDto>()).IsValid)
                .Returns(true);
            _user_QueryMock.Setup(uq => uq.CheckEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _company_QueryMock.Setup(cq => cq.FindCompanyByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Company() { CompanyId = Guid.NewGuid() });

            _role_QueryMock.Setup(rq => rq.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role() { RoleId = Guid.NewGuid(), RoleName = SeedRoles.ViewerName });

            _hashManagerMock.Setup(hm => hm.HashPassword(It.IsAny<string>()))
                .Returns("HashedPassword");

            _user_CommandMock.Setup(uc => uc.CreateUserAsync(It.IsAny<User>(), It.IsAny<UserInRole>()))
                .ThrowsAsync(new Exception("خطای ناشناخته رخ داد.", new Exception()));

            var request = new SignInServiceRequestDto
            {
                CompanyId = Guid.NewGuid(),
                UserEmail = "test@gmail.com",
                UserFullName = "Test",
                Password = "12345Ed@",
                ConPassword = "12345Ed@"
            };

            //act
            var result = await _signIn.CreateUserAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("خطای ناشناخته رخ داد.", result.Message);
        }
    }
}

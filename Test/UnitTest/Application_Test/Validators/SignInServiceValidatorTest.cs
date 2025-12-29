using Application.Dtos.Services.Commands.SignInService;
using Application.MediatR.Services.Commands.SignInService;
using Application.Validators.Commands.SignInService;
using FluentValidation.TestHelper;

namespace Application_Test.Validators
{
    public class SignInServiceValidatorTest
    {
        [Fact]
        public void UserFullName_IsNull_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = string.Empty,
                UserEmail = "Test@Email",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid()
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.UserFullName)
                .WithErrorMessage("لطفا نام و نام خانوادگی خود را وارد کنید");
        }

        [Fact]
        public void UserFullName_MaximumLength_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = new string('a', 51),
                UserEmail = "Test@Email",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid()
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.UserFullName)
                .WithErrorMessage("نام و نام خانوادگی باید کم تر 50 کاراکتر باشد");
        }

        [Fact]
        public void UserFullName_SpecialChar_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "a#",
                UserEmail = "Test@Email",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid()
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.UserFullName)
                .WithErrorMessage("لطفا نام و نام خانوادگی را به درستی وارد کنید");
        }

        [Fact]
        public void UserEmail_IsNull_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = string.Empty,
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid()
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.UserEmail)
                .WithErrorMessage("لطفا ایمیل خود را وارد کنید");
        }

        [Fact]
        public void UserEmail_TypeMissmatch_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "WrongEmail",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid()
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.UserEmail)
                .WithErrorMessage("لطفا ایمیل خودرا به درستی وارد کنید");
        }

        [Fact]
        public void UserEmail_MaximumLength_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = new string('a', 100) + "@Gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid()
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.UserEmail)
                .WithErrorMessage("ایمیل نمی تواند بیش از 100 کاراکتر باشد");
        }

        [Fact]
        public void CompanyId_IsNull_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.Empty
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.CompanyId)
                .WithErrorMessage("لطفا آی دی شرکت خود را وارد کنید");
        }

        [Fact]
        public void Password_IsNull_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = string.Empty,
                ConPassword = string.Empty,
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("لطفا رمزعبور را وارد کنید");
        }

        [Fact]
        public void Password_Is_Not_Equal_To_ConPassword_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "123456789Ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمزعبور و تکرار آن برابر نیست");
        }

        [Fact]
        public void Password_No_Uppercase_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345ed@",
                ConPassword = "12345ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد");
        }

        [Fact]
        public void Password_No_Lowercase_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345ED@",
                ConPassword = "12345ED@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد");
        }

        [Fact]
        public void Password_Password_Structure_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "abcdefG@",
                ConPassword = "abcdefG@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمز عبور باید حداقل یک عدد داشته باشد");
        }

        [Fact]
        public void Password_Minimum_Length_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "1Ed@",
                ConPassword = "1Ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمز عبور باید حداقل 8 کاراکتر باشد");
        }

        [Fact]
        public void Password_Maximum_Length_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = new string('1', 64) + "Ed@",
                ConPassword = new string('1', 64) + "Ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد");
        }

        [Fact]
        public void Password_At_Least_One_Special_Char_Error()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "123456Ed",
                ConPassword = "123456Ed",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(v => v.Dto.Password)
                .WithErrorMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد");
        }

        [Fact]
        public void Successfull_Validation()
        {
            //arrange
            var validator = new SignInServiceValidator();
            var model = new SignInServiceCommand(new SignInServiceRequestDto
            {
                UserFullName = "Test",
                UserEmail = "Test@gmail.com",
                Password = "12345Ed@",
                ConPassword = "12345Ed@",
                CompanyId = Guid.NewGuid(),
            });

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

using Domain.Entities.Common;
using Domain.Entities.ServiceManagement;

namespace Domain.Entities.UserManagement
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserFullName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public bool UserEmailConfirmed { get; set; } = false;
        public string UserPassword { get; set; } = null!;
        public int UserLogOutVersion { get; set; }

        public Company UserCompany { get; set; } = null!;
        public Guid UserCompanyId { get; set; } = SeedCompanies.DefaultCompanyId;

        public UserInRole UserInRole { get; set; } = null!;   // 1 user to 1 userInRole

        public List<UserToken> UserTokens { get; set; } = [];  //1 User to many UserTokens

        public void ChangeUserEmailConfirmedState()   //method for automated userEmailConfirmation update
        {
            UserEmailConfirmed = !UserEmailConfirmed;
            SetUpdatedAt();
        }

        public void CountUserLogOutVersion()   //method for automated UserLogOutVersion for Jwt logout proccess
        {
            UserLogOutVersion++;
            SetUpdatedAt();
        }

        public void SetUserInRole(UserInRole userInRole)   //method for automated SetUserInRole for User
        {
            UserInRole = userInRole;
            SetUpdatedAt();
        }

        //creator method
        public static User Create(string userFullName, string userEmail, string userPassword, Guid userCompanyId)
        {
            if (string.IsNullOrEmpty(userFullName) || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPassword))
                throw new ArgumentNullException("تمامی مقادیر را پر کنید");

            return new User
            {
                UserId = Guid.NewGuid(),
                UserFullName = userFullName,
                UserEmail = userEmail,
                UserEmailConfirmed = false,
                UserPassword = userPassword,
                UserCompanyId = userCompanyId == Guid.Empty ? SeedCompanies.DefaultCompanyId : userCompanyId,
                CreatedAt = DateTime.UtcNow,
            };
        }

        //add Company to user
        public void AssignCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("تمامی مقادیر را پر کنید");

            UserCompany = company;
            SetUpdatedAt();
        }

        //update password
        public void UpdatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("تمامی مقادیر را پر کنید");

            UserPassword = password;
            SetUpdatedAt();
        }
    }
}

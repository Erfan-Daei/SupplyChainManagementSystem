using Domain.Entities.Common;

namespace Infrastructure.Auth
{
    public class AuthPolicy
    {
        // Policy Name
        public const string SuperAdminsOnlyName = "SuperAdminsOnly";
        public const string AdminsOnlyName = "AdminsOnly";
        public const string UserOrHigherName = "UserOrHigher";

        // Policy Role
        public static List<string> SuperAdminsOnlyPolicy = [SeedRoles.AdminName];
        public static List<string> AdminsOnlyPolicy = [SeedRoles.AdminName, SeedRoles.CompanyAdminName];
        public static List<string> UserOrHigherPolicy = [SeedRoles.AdminName, SeedRoles.CompanyAdminName, SeedRoles.CompanyUserName];
    }
}

namespace Domain.Entities.Common
{
    public class SeedRoles   //default sysytem Roles
    {
        // GUID
        public static readonly Guid AdminId = Guid.Parse("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11");
        public static readonly Guid CompanyAdminId = Guid.Parse("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22");
        public static readonly Guid CompanyUserId = Guid.Parse("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33");
        public static readonly Guid ViewerId = Guid.Parse("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44");

        // Name
        public const string AdminName = "Admin";
        public const string CompanyAdminName = "CompanyAdmin";
        public const string CompanyUserName = "CompanyUser";
        public const string ViewerName = "Viewer";
    }
}

namespace Application.Interfaces.Database
{
    public interface IDatabaseContext_UserInfo   //interface to save userInfo for automated log process
    {
        public Guid UserId { get; }
        public string UserFullName { get; }
        public Guid RoleId { get; }
        public string RoleName { get; }
    }
}

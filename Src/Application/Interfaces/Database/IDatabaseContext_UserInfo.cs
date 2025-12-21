namespace Application.Interfaces.Database
{
    public interface IDatabaseContext_UserInfo
    {
        public Guid UserId { get; }
        public string UserFullName { get; }
        public Guid RoleId { get; }
        public string RoleName { get; }
    }
}

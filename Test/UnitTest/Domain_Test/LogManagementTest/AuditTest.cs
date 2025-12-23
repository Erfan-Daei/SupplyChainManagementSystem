using Domain.Entities.LogManagement;

namespace Domain_Test.LogManagementTest
{
    public class AuditTest
    {
        [Fact]
        public void Constructor_Should_Work_Correctly()   //check all properties get value correctly
        {
            //arrange
            var UserId = Guid.NewGuid();
            var UserFullName = "ErfanDaei";
            var RoleId = Guid.Parse("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11");
            var RoleName = "Admin";
            var Action = "Add";
            var ActionOnEntity = "Service";

            //act
            var _Audit = new Audit(UserId, UserFullName, RoleId, RoleName, Action, ActionOnEntity);

            //assert
            Assert.NotEqual(Guid.Empty, _Audit.AuditId);   //AuditId should have value
            Assert.Equal(UserId, _Audit.UserId);
            Assert.Equal(UserFullName, _Audit.UserFullName);
            Assert.Equal(RoleId, _Audit.RoleId);
            Assert.Equal(RoleName, _Audit.RoleName);
            Assert.Equal(Action, _Audit.Action);
            Assert.Equal(ActionOnEntity, _Audit.ActionOnEntity);
            Assert.True(_Audit.ActionAtTime <= DateTime.UtcNow);   //ActionAtTime should have Value
        }

        [Fact]
        public void Audit_Cant_Be_Change()   //check all properties are private set and cannot be changed
        {
            //assert
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.AuditId))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.UserId))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.UserFullName))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.RoleId))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.RoleName))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.Action))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.ActionOnEntity))?.GetSetMethod());
            Assert.Null(typeof(Audit).GetProperty(nameof(Audit.ActionAtTime))?.GetSetMethod());
        }

        [Fact]
        public void AuditId_Must_Be_Unique()   //constructor must generate new Guid
        {
            //arrange
            var _Audit1 = new Audit(Guid.NewGuid(), "ErfanDaei", Guid.Parse("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"), "Admin", "Add", "Service");
            var _Audit2 = new Audit(Guid.NewGuid(), "ErfanDaei", Guid.Parse("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"), "Admin", "Add", "Service");

            //act 
            var AuditId1 = _Audit1.AuditId;
            var AuditId2 = _Audit2.AuditId;

            //assert
            Assert.NotEqual(AuditId1, AuditId2);
        }
    }
}

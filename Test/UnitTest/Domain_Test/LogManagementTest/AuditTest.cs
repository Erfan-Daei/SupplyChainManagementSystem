using Domain.Entities.Common;
using Domain.Entities.LogManagement;

namespace Domain_Test.LogManagementTest
{
    public class AuditTest
    {
        [Fact]
        public void Constructor_Should_Create_Audit()   //check all properties get value correctly
        {
            //arrange
            var userId = Guid.NewGuid();
            var userFullName = "ErfanDaei";
            var roleId = SeedRoles.ViewerId;
            var roleName = SeedRoles.ViewerName;
            var action = "Added";
            var actionOnEntity = "Service";

            //act
            var _Audit = new Audit(userId, userFullName, roleId, roleName, action, actionOnEntity);

            //assert
            Assert.NotEqual(Guid.Empty, _Audit.AuditId);   //AuditId should have value
            Assert.Equal(userId, _Audit.UserId);
            Assert.Equal(userFullName, _Audit.UserFullName);
            Assert.Equal(roleId, _Audit.RoleId);
            Assert.Equal(roleName, _Audit.RoleName);
            Assert.Equal(action, _Audit.Action);
            Assert.Equal(actionOnEntity, _Audit.ActionOnEntity);
            Assert.True(_Audit.ActionAtTime <= DateTime.UtcNow);   //ActionAtTime should have Value
        }

        [Fact]
        public void Check_Audit_Cant_Be_Change()   //check all properties are private set and cannot be changed
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
            var audit1 = new Audit(Guid.NewGuid(), "ErfanDaei", SeedRoles.ViewerId, SeedRoles.ViewerName, "Added", "Service");
            var audit2 = new Audit(Guid.NewGuid(), "ErfanDaei", SeedRoles.ViewerId, SeedRoles.ViewerName, "Added", "Service");

            //act 
            var auditId1 = audit1.AuditId;
            var auditId2 = audit2.AuditId;

            //assert
            Assert.NotEqual(auditId1, auditId2);
        }
    }
}

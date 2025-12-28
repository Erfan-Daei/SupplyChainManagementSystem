using Domain.Entities.UserManagement;

namespace Domain_Test.Common
{
    public class BaseEntityTest
    {
        [Fact]
        public void SetUpdatedAt_Method_Will_Update_UpdateAt_Time()  //check baseEntity SetUpdatedAt method works properly
        {
            //arrange
            var user = new User();

            //act
            user.SetUpdatedAt();

            //assert
            Assert.True(user.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void SetDeletedAt_Method_Will_Soft_Delete()   //check baseEntity SetDeletedAt method works properly
        {
            //arrange
            var role = new Role();

            //act
            role.SetDeletedAt();

            //assert
            Assert.True(role.UpdatedAt <= DateTime.UtcNow);
            Assert.True(role.DeletedAt <= DateTime.UtcNow);
            Assert.True(role.IsDeleted);
        }
    }
}

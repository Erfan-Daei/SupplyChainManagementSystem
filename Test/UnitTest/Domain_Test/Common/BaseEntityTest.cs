using Domain.Entities.UserManagement;

namespace Domain_Test.Common
{
    public class BaseEntityTest
    {
        [Fact]
        public void SetUpdatedAt_Method_Work_Correctly()  //check baseEntity SetUpdatedAt method works properly
        {
            //arrange
            var _User = new User();

            //act
            _User.SetUpdatedAt();

            //assert
            Assert.True(_User.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void SetDeletedAt_Method_Work_Correctly()   //check baseEntity SetDeletedAt method works properly
        {
            //arrange
            var _Role = new Role();

            //act
            _Role.SetDeletedAt();

            //assert
            Assert.True(_Role.UpdatedAt <= DateTime.UtcNow);
            Assert.True(_Role.DeletedAt <= DateTime.UtcNow);
            Assert.True(_Role.IsDeleted);
        }
    }
}

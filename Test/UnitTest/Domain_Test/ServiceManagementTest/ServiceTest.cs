using Domain.Entities.ServiceManagement;

namespace Domain_Test.ServiceManagementTest
{
    public class ServiceTest
    {
        [Fact]
        public void SetServiceIsActive_Method_Work_Correctly()   //check SetServiceIsActive method works properly
        {
            //arrange
            var _Service = new Service();
            var RandomBool = new Random().Next(2) == 0;   //randomly set is active true false 
            _Service.ServiceIsActive = RandomBool;

            //act
            _Service.SetServiceIsActive();

            //assert
            Assert.True(_Service.ServiceIsActive != RandomBool);
            Assert.True(_Service.UpdatedAt <= DateTime.UtcNow);
        }
    }
}

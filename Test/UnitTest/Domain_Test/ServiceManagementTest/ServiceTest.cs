using Domain.Entities.ServiceManagement;

namespace Domain_Test.ServiceManagementTest
{
    public class ServiceTest
    {
        [Fact]
        public void ChangeServiceIsActiveState_Method_Work_Correctly()   //check SetServiceIsActive method works properly
        {
            //arrange
            var service = new Service();
            var randomBool = new Random().Next(2) == 0;   //randomly set is active true false 
            service.ServiceIsActive = randomBool;

            //act
            service.ChangeServiceIsActiveState();

            //assert
            Assert.True(service.ServiceIsActive != randomBool);
            Assert.True(service.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void Create_Method_Throw_Exception_For_Null_Values()
        {
            //arrange
            string serviceName = string.Empty; 
            string serviceDescription = string.Empty;

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var service = Service.Create(serviceName, serviceDescription);
            });

            Assert.Contains("لطفا تمام مقادیر را پر کنید", result.Message);
        }

        [Fact]
        public void Create_Mehod_Should_Create_Service()
        {
            //arrange
            string serviceName = "service";
            string serviceDescription = "it is service";

            //act
            var service = Service.Create(serviceName, serviceDescription);

            //assert
            Assert.NotEqual(Guid.Empty, service.ServiceId);
            Assert.Equal(serviceName, service.ServiceName);
            Assert.Equal(serviceDescription, service.ServiceDescription);
            Assert.True(DateTime.UtcNow >= service.CreatedAt);
        }
    }
}

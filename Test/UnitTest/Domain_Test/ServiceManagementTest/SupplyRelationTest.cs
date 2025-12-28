using Domain.Entities.Common;
using Domain.Entities.ServiceManagement;

namespace Domain_Test.ServiceManagementTest
{
    public class SupplyRelationTest
    {
        [Fact]
        public void ChangeSupplyRelationIsActiveState_Method_Work_Correctly()   //check SetSupplyRelationIsActive method works properly
        {
            //arrange
            var supplyRelation = new SupplyRelation();
            var randomBool = new Random().Next(2) == 0;   //randomly set is active true false 
            supplyRelation.SupplyRelationIsActive = randomBool;

            //act
            supplyRelation.ChangeSupplyRelationIsActiveState();

            //assert
            Assert.True(supplyRelation.SupplyRelationIsActive != randomBool);
            Assert.True(supplyRelation.UpdatedAt <=  DateTime.UtcNow);
        }

        [Fact]
        public void Create_Method_Should_Throw_Exception_When_Consumer_And_Supplier_Are_Equal()   //check that SupplyRelation constructor will properly detect Supplier and Consumer are equal and throw error
        {
            //arrange
            var service = new Service() { ServiceId = Guid.NewGuid()};
            var supplier = new Company { CompanyId = SeedCompanies.DefaultCompanyId };
            var consumer = new Company { CompanyId = SeedCompanies.DefaultCompanyId };

            //act & assert
            var result = Assert.Throws<InvalidOperationException>(() =>
            {
                var supplyRelation = SupplyRelation.Create(service.ServiceId, supplier.CompanyId, consumer.CompanyId);
            });

            Assert.Equal("سرویس دهنده و سرویس گیرنده نمیتوانند یکسان باشند", result.Message);
        }

        [Fact]
        public void Create_Method_Should_Throw_Exeption_for_Null_Values()   //check that SupplyRelation constructor will properly detect Supplier and Consumer are equal and throw error
        {
            //arrange
            var service = new Service() { ServiceId = Guid.Empty};
            var supplier = new Company() { CompanyId = Guid.Empty};
            var consumer = new Company() { CompanyId = Guid.Empty};

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var supplyRelation = SupplyRelation.Create(service.ServiceId, supplier.CompanyId, consumer.CompanyId);
            });

            Assert.Contains("لطفا تمام مقادیر را پر کنید", result.Message);
        }

        [Fact]
        public void Create_Relation_When_Supplier_And_Cunsumer_Are_Not_Equal()   //check that SupplyRelation constructor will properly when Supplier and Consumer are "not" equal
        {
            //arrange
            var service = new Service { ServiceId = Guid.NewGuid() };
            var supplier = new Company { CompanyId = Guid.NewGuid() };
            var consumer = new Company { CompanyId = Guid.NewGuid() };

            //act
            var supplyRelation = SupplyRelation.Create(service.ServiceId, supplier.CompanyId, consumer.CompanyId);

            //assert
            Assert.NotEqual(Guid.Empty, supplyRelation.SupplierCompanyId);
            Assert.Equal(supplyRelation.ServiceId, service.ServiceId);
            Assert.Equal(supplyRelation.ConsumerCompanyId, consumer.CompanyId);
            Assert.Equal(supplyRelation.SupplierCompanyId, supplier.CompanyId);
            Assert.True(supplyRelation.SupplyRelationIsActive);
            Assert.True(DateTime.UtcNow >= supplyRelation.CreatedAt);
        }
    }
}

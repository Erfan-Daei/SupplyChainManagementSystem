using Domain.Entities.ServiceManagement;

namespace Domain_Test.ServiceManagementTest
{
    public class SupplyRelationTest
    {
        [Fact]
        public void SetSupplyRelationIsActive_Method_Work_Correctly()   //check SetSupplyRelationIsActive method works properly
        {
            //arrange
            var _SupplyRelation = new SupplyRelation();
            var RandomBool = new Random().Next(2) == 0;   //randomly set is active true false 
            _SupplyRelation.SupplyRelationIsActive = RandomBool;

            //act
            _SupplyRelation.SetSupplyRelationIsActive();

            //assert
            Assert.True(_SupplyRelation.SupplyRelationIsActive != RandomBool);
            Assert.True(_SupplyRelation.UpdatedAt <=  DateTime.UtcNow);
        }

        [Fact]
        public void Throw_Error_When_Consumer_And_Supplier_Are_Equal()   //check that SupplyRelation constructor will properly detect Supplier and Consumer are equal and throw error
        {
            //arrange
            var _Service = new Service { SupplierCompanyId = Guid.NewGuid() };
            var _Consumer = new Company { CompanyId = _Service.SupplierCompanyId };

            //assert
            Assert.Throws<InvalidOperationException>(() =>
            new SupplyRelation(_Service, _Consumer)
            );
        }

        [Fact]
        public void Create_Relation_When_Supplier_And_Cunsumer_Are_Not_Equal()   //check that SupplyRelation constructor will properly when Supplier and Consumer are "not" equal
        {
            //arrange
            var _Service = new Service { ServiceId = Guid.NewGuid(), SupplierCompanyId = Guid.NewGuid() };
            var _Consumer = new Company { CompanyId = Guid.NewGuid() };

            //act
            var _SupplyRelation = new SupplyRelation(_Service, _Consumer);

            //assert
            Assert.Equal(_SupplyRelation.ServiceId, _Service.ServiceId);
            Assert.Equal(_SupplyRelation.Service, _Service);
            Assert.Equal(_SupplyRelation.ConsumerCompanyId, _Consumer.CompanyId);
            Assert.Equal(_SupplyRelation.ConsumerCompany, _Consumer);
            Assert.True(_SupplyRelation.SupplyRelationIsActive);
        }

        [Fact]
        public void Check_Empty_Constructor_Work_Correctly()   //check empty constructor for ef works properly 
        {
            //act
            var _SupplyRelation = new SupplyRelation();

            //assert
            Assert.NotNull(_SupplyRelation);
            Assert.Equal(default(Guid), _SupplyRelation.SupplyRelationId);
            Assert.Null(_SupplyRelation.Service);
            Assert.Equal(default(Guid), _SupplyRelation.ServiceId);
            Assert.Null(_SupplyRelation.ConsumerCompany);
            Assert.Equal(default(Guid), _SupplyRelation.ConsumerCompanyId);
            Assert.True(_SupplyRelation.SupplyRelationIsActive);
        }
    }
}

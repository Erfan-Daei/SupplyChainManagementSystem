using Domain.Entities.ServiceManagement;

namespace Domain_Test.ServiceManagementTest
{
    public class CompanyTest
    {
        [Fact]
        public void Null_CompanyName_Should_Throw_Exception()
        {
            //arrange
            string companyName = string.Empty;

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var company = Company.Create(companyName);
            });

            Assert.Contains("نام شرکت نمی تواند خالی باشد", result.Message);
        }
        [Fact]
        public void Create_Method_Should_Create_Company()
        {
            //arrange
            string companyName = "default";

            //act
            var company = Company.Create(companyName);

            //assert
            Assert.NotEqual(Guid.Empty, company.CompanyId);
            Assert.Equal(companyName, company.CompanyName);
            Assert.True(DateTime.UtcNow >= company.CreatedAt);
        }
    }
}

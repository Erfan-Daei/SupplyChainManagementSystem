using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class Service : BaseEntity
    {
        public Guid ServiceId { get; set; } = Guid.NewGuid();
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; } = true;

        // 1 service to many supplyRelation
        public ICollection<SupplyRelation> SupplyRelations { get; set; } = [];

        public void ChangeServiceIsActiveState()   //method for automated serviceActivation update
        {
            ServiceIsActive = !ServiceIsActive;
            SetUpdatedAt();
        }

        //creator method
        public static Service Create(string serviceName, string serviceDescription)
        {
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(serviceDescription))
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            return new Service()
            {
                ServiceId = Guid.NewGuid(),
                ServiceName = serviceName,
                ServiceDescription = serviceDescription,
                ServiceIsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

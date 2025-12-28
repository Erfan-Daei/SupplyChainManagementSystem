namespace Domain.Entities.Common
{
    public class BaseEntity   //common properties for most of entities
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public void SetUpdatedAt()   //method for automated table UpdateAt property
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDeletedAt()   //method for automated soft delete functions
        {
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = DateTime.UtcNow;
            IsDeleted = true;
        }
    }
}

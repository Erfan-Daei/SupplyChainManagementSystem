using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Common
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public void SetDeletedAt()
        {
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = DateTime.UtcNow;
            IsDeleted = true;
        }
    }
}

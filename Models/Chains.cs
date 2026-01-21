using System;
using System.Collections.Generic;

namespace StoreManager.Models
{
    public class Chain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public List<Guid> StoreIds { get; set; } = new();
    }
}
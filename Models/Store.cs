using System;
using System.Collections.Generic;

namespace StoreManager.Models
{
    public class Store
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? Storenumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public string City { get; set; } = string.Empty;
        public int? PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string StoreOwner { get; set; } = string.Empty;
        public Guid? ChainId { get; set; } = null;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
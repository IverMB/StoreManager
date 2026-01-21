using System;
using System.Collections.Generic;

namespace StoreManager.Models
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password{ get; set; } = string.Empty;
        public string Role { get; set; } = Roles.Support;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }

    public class Roles
    {
        public const string Administrator = "Administrator";
        public const string Support = "Support";
        public const string User = "User";
    }
}
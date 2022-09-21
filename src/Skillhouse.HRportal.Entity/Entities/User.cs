using System;
using System.Collections.Generic;

namespace Skillhouse.HRportal.Entity
{
    public partial class User
    {
        public User()
        {
            UserLanguages = new HashSet<UserLanguage>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Status { get; set; }
        public int? UnsuccessfulLoginAttempts { get; set; }
        public DateTime? LastLogin { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Skillhouse.HRportal.Entity
{
    public partial class Language
    {
        public Language()
        {
            UserLanguages = new HashSet<UserLanguage>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
    }
}

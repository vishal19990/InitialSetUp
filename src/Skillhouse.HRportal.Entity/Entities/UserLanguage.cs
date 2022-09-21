using System;
using System.Collections.Generic;

namespace Skillhouse.HRportal.Entity
{
    public partial class UserLanguage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

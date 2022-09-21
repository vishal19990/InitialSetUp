namespace Skillhouse.HRportal.Core.Models
{
    public class ApplicationContext
    {
        public string CorrelationId { get; } = Guid.NewGuid().ToString();
        public int UserId { get; set; } = -1;

        public string UserEmail { get; set; } = null!;

        public bool SuppressSoftDeleteFilters { get; set; }
    }
}

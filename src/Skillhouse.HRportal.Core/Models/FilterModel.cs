namespace Skillhouse.HRportal.Core.Models
{
    public class FilterModel
    {
        public int PageNumber { get; set; }

        public string SortMember { get; set; } = null!;

        public int PageSize { get; set; }

        public bool SortDescending { get; set; }

        public List<FilterOption> Filters { get; set; }

    }

    public class FilterOption
    {
        public string Value { get; set; } = null!;

        public string Property { get; set; } = null!;

        public string? Operator { get; set; }
    }
}

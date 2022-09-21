namespace Skillhouse.HRportal.Core.Models
{
    public class PagedResultModel<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Records { get; set; }
        public int TotalPages { get; set; }
    }
}

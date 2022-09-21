namespace Skillhouse.HRportal.Core.Constraints
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}

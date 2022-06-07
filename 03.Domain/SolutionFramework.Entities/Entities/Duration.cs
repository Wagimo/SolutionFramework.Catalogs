using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class Duration : IEntityBase<Guid, string>
    {
        public Duration()
        {
            DurationDetail = new List<DurationDetail>();
        }
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Period { get; set; } = string.Empty;
        public List<DurationDetail> DurationDetail { get; set; }
    }
}

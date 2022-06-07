using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class DurationDetail : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public Guid IdDuration { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public Duration Duration { get; set; }
    }
}

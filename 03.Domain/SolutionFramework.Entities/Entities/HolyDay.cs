using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class HolyDay : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public DateTime Holyday { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class Activity : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

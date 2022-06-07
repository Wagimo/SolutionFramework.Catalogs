using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class MenuItem : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public Guid? IdParent { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        public string Module { get; set; } = string.Empty;

    }
}

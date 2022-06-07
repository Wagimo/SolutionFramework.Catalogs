using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class DocumentType : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool HasTracking { get; set; }
        public string RolResponsible { get; set; } = string.Empty;
    }
}

using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class ApproverMatrix : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public Guid IdArea { get; set; }
        public string IdFirstApprover { get; set; } = string.Empty;
        public string IdSecondApprover { get; set; } = string.Empty;
        public Area Area { get; set; }
    }
}

using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class Area : IEntityBase<Guid, string>
    {
        public Area()
        {
            ApproverMatrix = new List<ApproverMatrix>();
        }
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IdManager { get; set; } = string.Empty;
        public Guid IdCompany { get; set; }
        public Company Company { get; set; }
        public decimal? ApprovalLimitAmount { get; set; }
        public List<ApproverMatrix> ApproverMatrix { get; set; }

    }
}

using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class ContractType : IEntityBase<Guid, string>
    {
        public ContractType()
        {
            ContractTemplates = new List<ContractTemplate>();
        }
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }


        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TrackingDays { get; set; }
        public List<ContractTemplate> ContractTemplates { get; set; }
    }
}

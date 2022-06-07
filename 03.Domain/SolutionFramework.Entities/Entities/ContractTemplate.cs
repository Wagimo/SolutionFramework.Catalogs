using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class ContractTemplate : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public Guid IdContractType { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public byte[] Content { get; set; }
        public ContractType ContractType { get; set; }

    }
}

using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class OptionList : IEntityBase<Guid, string>
    {
        public OptionList()
        {
            Details = new List<OptionListDetail>();
        }
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public bool Private { get; set; }
        public List<OptionListDetail> Details { get; set; }
    }
}

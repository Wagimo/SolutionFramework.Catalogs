using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class Company : IEntityBase<Guid, string>
    {
        public Company()
        {
            Areas = new List<Area>();
        }
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string LegalRepresentative { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public List<Area> Areas { get; set; }
    }
}

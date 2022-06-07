using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.Entities.Entities
{
    public class Provider : IEntityBase<Guid, string>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        public string Type { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string ClientCode { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string LegalRepresentative { get; set; } = string.Empty;
        public string IdLegalRepresentative { get; set; } = string.Empty;
        public string EmailLegalRepresentative { get; set; } = string.Empty;
    }
}

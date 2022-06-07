using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class ProviderEntityConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ConfigurationBase<Guid, string, Provider>();
            builder.ToTable("Providers");
            builder.Property(x => x.Type).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.BusinessName).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.ClientCode).HasColumnType("varchar(30)").IsRequired();
            builder.Property(x => x.Nit).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Address).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.City).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.ContactName).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.EmailAddress).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.LegalRepresentative).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.IdLegalRepresentative).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.EmailLegalRepresentative).HasColumnType("varchar(50)").IsRequired();

        }
    }
}

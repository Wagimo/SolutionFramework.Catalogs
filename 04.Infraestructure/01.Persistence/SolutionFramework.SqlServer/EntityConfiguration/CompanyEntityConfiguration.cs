using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ConfigurationBase<Guid, string, Company>();
            builder.ToTable("Companies");
            builder.Property(x => x.Name).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Nit).HasColumnType("varchar(20)").IsRequired(false);
            builder.Property(x => x.LegalRepresentative).HasColumnType("varchar(70)").IsRequired();
            builder.Property(x => x.Address).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Fax).HasColumnType("varchar(20)").IsRequired(false);
            builder.Property(x => x.City).HasColumnType("varchar(30)").IsRequired();

        }
    }
}

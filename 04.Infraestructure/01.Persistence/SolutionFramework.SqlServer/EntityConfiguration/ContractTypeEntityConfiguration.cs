using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class ContractTypeEntityConfiguration : IEntityTypeConfiguration<ContractType>
    {
        public void Configure(EntityTypeBuilder<ContractType> builder)
        {
            builder.ConfigurationBase<Guid, string, ContractType>();
            builder.ToTable("ContractTypes");
            builder.Property(x => x.CategoryName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.TrackingDays).HasColumnType("int").IsRequired();

        }
    }
}

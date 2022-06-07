using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class OptionListEntityConfiguration : IEntityTypeConfiguration<OptionList>
    {
        public void Configure(EntityTypeBuilder<OptionList> builder)
        {
            builder.ConfigurationBase<Guid, string, OptionList>();
            builder.ToTable("OptionList");
            builder.Property(x => x.Key).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Value).HasColumnType("varchar(MAX)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(250)").IsRequired(false);
            builder.Property(x => x.FieldType).HasColumnType("varchar(50)").IsRequired(false);
            builder.Property(x => x.Private).HasColumnType("bit").IsRequired();

        }
    }
}

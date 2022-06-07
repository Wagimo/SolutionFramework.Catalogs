using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class OptionListDetailEntityConfiguration : IEntityTypeConfiguration<OptionListDetail>
    {
        public void Configure(EntityTypeBuilder<OptionListDetail> builder)
        {
            builder.ConfigurationBase<Guid, string, OptionListDetail>();
            builder.ToTable("OptionListDetail");
            builder.Property(x => x.Key).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Value).HasColumnType("varchar(MAX)").IsRequired();

            builder
                .HasOne(x => x.OptionList)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.IdOptionList)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

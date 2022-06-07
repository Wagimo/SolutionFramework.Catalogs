using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class HolyDayEntityConfiguration : IEntityTypeConfiguration<HolyDay>
    {
        public void Configure(EntityTypeBuilder<HolyDay> builder)
        {
            builder.ConfigurationBase<Guid, string, HolyDay>();
            builder.ToTable("HolyDays");
            builder.Property(x => x.Holyday).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(150)").IsRequired(false);

        }
    }
}

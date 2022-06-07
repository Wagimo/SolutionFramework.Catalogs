using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class ActivityEntityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ConfigurationBase<Guid, string, Activity>();
            builder.ToTable("Activities");
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(250)").IsRequired();
        }
    }
}

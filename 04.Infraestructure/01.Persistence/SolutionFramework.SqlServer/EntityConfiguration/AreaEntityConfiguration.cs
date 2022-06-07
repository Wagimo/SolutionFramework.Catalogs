using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;
namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class AreaEntityConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ConfigurationBase<Guid, string, Area>();
            builder.ToTable("Areas");
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.IdManager).HasColumnType("varchar(128)").IsRequired(false);
            builder.Property(x => x.ApprovalLimitAmount).HasColumnType("decimal(18,2)").IsRequired(false);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Areas)
                .HasForeignKey(x => x.IdCompany)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

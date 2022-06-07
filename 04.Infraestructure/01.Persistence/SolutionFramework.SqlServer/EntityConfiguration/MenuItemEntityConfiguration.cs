using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class MenuItemEntityConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ConfigurationBase<Guid, string, MenuItem>();
            builder.ToTable("MenuItems");
            builder.Property(x => x.Title).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Module).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.Route).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Icon).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.OrderIndex).HasColumnType("int").IsRequired();
            builder.Property(x => x.IdParent).HasColumnType("uniqueidentifier").IsRequired(false);
        }
    }
}

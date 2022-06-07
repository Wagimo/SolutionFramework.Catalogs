using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class DocumentTypeEntityConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.ConfigurationBase<Guid, string, DocumentType>();
            builder.ToTable("DocumentTypes");
            builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Category).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.HasTracking).HasColumnType("bit").IsRequired();
            builder.Property(x => x.RolResponsible).HasColumnType("varchar(50)").IsRequired(false);

        }
    }
}

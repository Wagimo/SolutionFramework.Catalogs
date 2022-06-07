using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class EmailTemplateEntityConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.ConfigurationBase<Guid, string, EmailTemplate>();
            builder.ToTable("EmailTemplates");
            builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Content).HasColumnType("image").IsRequired();

        }
    }
}

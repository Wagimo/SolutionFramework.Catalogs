using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class ContractTemplateEntityConfiguration : IEntityTypeConfiguration<ContractTemplate>
    {
        public void Configure(EntityTypeBuilder<ContractTemplate> builder)
        {
            builder.ConfigurationBase<Guid, string, ContractTemplate>();
            builder.ToTable("ContractTemplates");
            builder.Property(x => x.TemplateName).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Content).HasColumnType("image").IsRequired();
            builder.HasOne(x => x.ContractType)
              .WithMany(x => x.ContractTemplates)
              .HasForeignKey(x => x.IdContractType)
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
